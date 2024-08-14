using AutoFixture;
using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using BookManager.Infrastructure.Persistence.Repository.Command;
using BookManager.Infrastructure.Persistence.Repository.Query;
using BookManager.Infrastructure.Test.Repository;

namespace Command
{
    public class BookRepositoryIntegrationTest : ConfigureTest
    {
        protected Book Book;

        protected readonly IBookRepository _bookRepository;
        protected readonly IBookQuery _bookQuery;

        protected readonly IAuthorRepository _authorRepository;
        protected readonly IAuthorQuery _authorQuery;

        protected readonly ISubjectRepository _subjectRepository;
        protected readonly ISubjectQuery _subjectQuery;

        protected readonly Fixture _fixture;

        public BookRepositoryIntegrationTest()
        {
            _fixture = new();

            _bookRepository = new BookRepository(_dbConnection);
            _bookQuery = new BookQuery(_dbConnection);

            _authorRepository = new AuthorRepository(_dbConnection);
            _authorQuery = new AuthorQuery(_dbConnection);

            _subjectRepository = new SubjectRepository(_dbConnection);
            _subjectQuery = new SubjectQuery(_dbConnection);
        }

        private Book CreateBook()
        {
            var random = new Random();

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customize<Book>(c => c
              .With(x => x.Title, _fixture.Create<string>().PadRight(40, 'x')[..(40 - nameof(Book.Title).Length)])
              .With(x => x.PublisherBook, _fixture.Create<string>().PadRight(40, 'x')[..(40 - nameof(Book.PublisherBook).Length)])
              .With(x => x.YearPublication, random.Next(1000, DateTime.Now.Year).ToString())
            );

            return _fixture.Create<Book>();
        }

        private Author CreateAuthor()
        {
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customize<Author>(c => c
                .With(x => x.Name, _fixture.Create<string>().PadRight(40, 'x').Substring(0, 40))
            );

            return _fixture.Create<Author>(); ;
        }

        private Subject CreateSubject()
        {
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customize<Subject>(c => c
                .With(x => x.Description, _fixture.Create<string>().PadRight(20, 'x').Substring(0, 20))
            );

            return _fixture.Create<Subject>(); ;
        }

        [Fact]
        public async Task Insert_ShouldInsertAsync()
        {
            // Arrange                           
            Book = CreateBook();

            // Act
            var exception = await Record.ExceptionAsync(() => _bookRepository.InsertAsync(Book));
            var insertedBook = await _bookQuery.Get(Book.Id);

            await _bookRepository.DeleteAsync(Book.Id);

            // Assert
            Assert.Null(exception);
            Assert.NotNull(insertedBook);
            Assert.Equal(Book.Title, insertedBook.Title);
        }

        [Fact]
        public async Task Update_ShouldUpdateAsync()
        {
            // Arrange                         
            Book = CreateBook();

            await _bookRepository.InsertAsync(Book);

            var updateAuthor = CreateBook();
            updateAuthor.Id = Book.Id;

            // Act
            var exception = await Record.ExceptionAsync(() => _bookRepository.UpdateAsync(updateAuthor));
            var updatedBook = await _bookQuery.Get(Book.Id);
            await _bookRepository.DeleteAsync(Book.Id);

            // Assert                          
            Assert.Null(exception);
            Assert.NotNull(updatedBook);
            Assert.NotEqual(Book.Title, updateAuthor.Title);
            Assert.Equal(updateAuthor.Title, updatedBook.Title);
        }

        [Fact]
        public async Task Delete_ShouldDeleteAsync()
        {
            // Arrange             
            Book = CreateBook();

            await _bookRepository.InsertAsync(Book);

            // Act
            var exception = await Record.ExceptionAsync(() => _bookRepository.DeleteAsync(Book.Id));
            var deletedAuthor = await _bookQuery.Get(Book.Id);

            // Assert
            Assert.Null(exception);
            Assert.Null(deletedAuthor);
        }

        [Fact]
        public async Task InsertAuthorSubject_ShouldInsertAsync()
        {
            // Arrange                           
            Book = CreateBook();
            Book.Authors = [CreateAuthor()];

            Book.Subjects = [CreateSubject()];

            // Act
            await _bookRepository.InsertAsync(Book);
            await _authorRepository.InsertAsync(Book.Authors.First());
            await _subjectRepository.InsertAsync(Book.Subjects.First());

            var exceptionAuthorBook = await Record.ExceptionAsync(() => _bookRepository.InsertAuthorBookAsync(Book));
            var exceptionSubjectBook = await Record.ExceptionAsync(() => _bookRepository.InsertSubjectBookAsync(Book));

            var insertedBook = await _bookQuery.Get(Book.Id);

            await _bookRepository.DeleteAsync(Book.Id);
            await _authorRepository.DeleteAsync(Book.Authors.First().Id);
            await _subjectRepository.DeleteAsync(Book.Subjects.First().Id);

            // Assert              
            Assert.Null(exceptionAuthorBook);
            Assert.Null(exceptionSubjectBook);
            Assert.NotNull(insertedBook);
            Assert.Equal(Book.Title, insertedBook.Title);
        }

        [Fact]
        public async Task UpdateAuthorSubject_ShouldUpdateAsync()
        {
            // Arrange                           
            Book = CreateBook();
            Book.Authors = [CreateAuthor()];
            Book.Subjects = [CreateSubject()];

            // Act
            await _bookRepository.InsertAsync(Book);
            await _authorRepository.InsertAsync(Book.Authors.First());
            await _subjectRepository.InsertAsync(Book.Subjects.First());

            await _bookRepository.InsertAuthorBookAsync(Book);
            await _bookRepository.InsertSubjectBookAsync(Book);

            var exceptionAuthorBook = await Record.ExceptionAsync(() => _bookRepository.DeleteAuthorBookAsync(Book.Id));
            var exceptionSubjectBook = await Record.ExceptionAsync(() => _bookRepository.DeleteSubjectBookAsync(Book.Id));

            var insertedBook = await _bookQuery.Get(Book.Id);

            await _bookRepository.DeleteAsync(Book.Id);
            await _authorRepository.DeleteAsync(Book.Authors.First().Id);
            await _subjectRepository.DeleteAsync(Book.Subjects.First().Id);

            // Assert              
            Assert.Null(exceptionAuthorBook);
            Assert.Null(exceptionSubjectBook);
            Assert.Empty(insertedBook.Authors);
            Assert.Empty(insertedBook.Subjects);
        }
    }
}