using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using Dapper;
using System.Data;

namespace BookManager.Infrastructure.Persistence.Repository.Query
{
    public class BookQuery(IDbConnection conn) : IBookQuery
    {
        private readonly IDbConnection _conn = conn;
        private const string mainQuery = @"SELECT 
	                 l.CodL Id
                    ,l.Titulo Title
                    ,l.Editora PublisherBook
                    ,l.Edicao Edition
                    ,l.AnoPublicacao YearPublication
	                ,au.CodAu Id
	                ,au.Nome Name
	                ,[as].codAs Id
	                ,[as].Descricao Description
                FROM Livro l (NOLOCK)
	                LEFT JOIN Livro_Autor lAu (NOLOCK)
		                ON l.CodL = lAu.Livro_CodL
	                LEFT JOIN Autor au (NOLOCK)
		                ON lAu.Autor_CodAu = au.CodAu
	                LEFT JOIN Livro_Assunto lAs (NOLOCK)
		                ON l.CodL = lAs.Livro_CodL
	                LEFT JOIN Assunto as [as] (NOLOCK)
		                ON lAs.Assunto_CodAs = [as].codAs
                ";
        public async Task<Book?> Get(int id)
        {
            var bookDictionary = new Dictionary<int, Book>();

            await _conn.QueryAsync(
               $@"{mainQuery}  WHERE l.CodL = @CodL "
           , (Func<Book, Author, Subject, Book>)((book, author, subject) =>
           {
               var currentBook = FillGetBook(book, author, subject, bookDictionary);

               return currentBook;
           }),
               new
               {
                   CodL = id
               },
               splitOn: "Id,Id");

            return bookDictionary.Values.FirstOrDefault();
        }

        public async Task<IEnumerable<Book>> Get(Book book)
        {

            var query = $"{mainQuery}  WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(book.Title))
            {
                query += "AND Titulo LIKE @Titulo ";
            }

            if (!string.IsNullOrWhiteSpace(book.PublisherBook))
            {
                query += "AND Editora LIKE @Editora ";
            }

            if (book.Edition > 0)
            {
                query += "AND Edicao = @Edicao ";
            }

            if (!string.IsNullOrWhiteSpace(book.YearPublication))
            {
                query += "AND AnoPublicacao = @AnoPublicacao ";
            }
            var bookDictionary = new Dictionary<int, Book>();
            await _conn.QueryAsync<Book, Author, Subject, Book>(query, (book, author, subject) =>
            {
                var currentBook = FillGetBook(book, author, subject, bookDictionary);
                return currentBook;
            },
                new
                {
                    Titulo = $"%{book.Title}%",
                    Editora = $"%{book.PublisherBook}%",
                    Edicao = book.Edition,
                    AnoPublicacao = book.YearPublication
                },
               splitOn: "Id,Id");

            return bookDictionary.Values.Select(bookValues => bookValues);
        }

        private static Book FillGetBook(Book book, Author author, Subject subject, Dictionary<int, Book> bookDictionary)
        {
            if (!bookDictionary.TryGetValue(book.Id, out var currentBook))
            {
                currentBook = book;
                currentBook.Authors = [];
                currentBook.Subjects = [];

                bookDictionary.Add(currentBook.Id, currentBook);
            }
            if (!currentBook.Authors.Any(authorItem => authorItem.Id == author.Id) && author != null)
            {
                currentBook.Authors.Add(author);
            }

            if (!currentBook.Subjects.Any(subjectItem => subjectItem.Id == subject.Id) && subject != null)
            {
                currentBook.Subjects.Add(subject);
            }

            return currentBook;
        }
    }
}
