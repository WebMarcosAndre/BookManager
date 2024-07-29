using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using Dapper;
using System.Data;

namespace BookManager.Infrastructure.Persistence.Repository.Command
{
    public class BookRepository(IDbConnection conn) : IBookRepository
    {
        private readonly IDbConnection _conn = conn;

        public async Task InsertAsync(Book book)
        {
            book.Id = await _conn.QuerySingleAsync<int>(
                @"INSERT INTO Livro (
                    Titulo, 
                    Editora, 
                    Edicao, 
                    AnoPublicacao
                ) OUTPUT INSERTED.CodL VALUES (
                    @Titulo, 
                    @Editora, 
                    @Edicao, 
                    @AnoPublicacao)",
                new
                {
                    Titulo = book.Title,
                    Editora = book.PublisherBook,
                    Edicao = book.Edition,
                    AnoPublicacao = book.YearPublication
                });
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE Livro 
                    SET Titulo = @Titulo, 
                        Editora = @Editora, 
                        Edicao = @Edicao, 
                        AnoPublicacao = @AnoPublicacao 
                WHERE CodL = @CodL",
                new
                {
                    CodL = book.Id,
                    Titulo = book.Title,
                    Editora = book.PublisherBook,
                    Edicao = book.Edition,
                    AnoPublicacao = book.YearPublication
                }) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _conn.ExecuteAsync("DELETE FROM Livro WHERE CodL = @CodL",
              new
              {
                  CodL = id
              }) > 0;
        }

        public async Task DeleteAuthorBookAsync(int id)
        {
            await _conn.ExecuteAsync(
               @"DELETE FROM Livro_Autor WHERE Livro_CodL = @Livro_CodL",
            new
            {
                Livro_CodL = id
            });
        }

        public async Task InsertAuthorBookAsync(Book book)
        {
            foreach (var author in book.Authors)
            {
                await _conn.ExecuteAsync(
                   @"INSERT INTO Livro_Autor (Autor_CodAu, Livro_CodL) VALUES (@Autor_CodAu,@Livro_CodL)",
                new
                {
                    Autor_CodAu = author.Id,
                    Livro_CodL = book.Id
                });
            }
        }

        public async Task DeleteSubjectBookAsync(int id)
        {
            await _conn.ExecuteAsync(
               @"DELETE Livro_Assunto WHERE Livro_CodL = @Livro_CodL",
            new
            {
                Livro_CodL = id
            });
        }

        public async Task InsertSubjectBookAsync(Book book)
        {
            foreach (var subject in book.Subjects)
            {
                await _conn.ExecuteAsync(
                   @"INSERT INTO Livro_Assunto (Assunto_CodAs, Livro_CodL) VALUES (@Assunto_CodAs,@Livro_CodL)",
                new
                {
                    Assunto_CodAs = subject.Id,
                    Livro_CodL = book.Id
                });
            }
        }
    }
}
