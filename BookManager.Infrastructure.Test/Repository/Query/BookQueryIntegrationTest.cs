using AutoFixture;
using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using BookManager.Infrastructure.Persistence.Repository.Query;
using BookManager.Infrastructure.Test.Repository;

namespace Query
{
    public class BookQueryIntegrationTest : ConfigureTest
    {
        protected Book Book;

        protected readonly IBookQuery _bookQuery;

        protected readonly Fixture _fixture;

        public BookQueryIntegrationTest()
        {
            _fixture = new();

            _bookQuery = new BookQuery(_dbConnection);
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

        [Fact]
        public async Task GetById_ShouldNotThrowExceptionAsync()
        {
            // Arrange                           
            Book = CreateBook();

            // Act
            var exception = await Record.ExceptionAsync(() => _bookQuery.Get(Book.Id));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetByFilter_ShouldNotThrowExceptionAsync()
        {
            // Arrange                           
            Book = CreateBook();

            // Act
            var exception = await Record.ExceptionAsync(() => _bookQuery.Get(Book));

            // Assert
            Assert.Null(exception);
        }
    }
}