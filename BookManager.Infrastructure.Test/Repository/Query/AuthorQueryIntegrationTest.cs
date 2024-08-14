using AutoFixture;
using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using BookManager.Infrastructure.Persistence.Repository.Query;
using BookManager.Infrastructure.Test.Repository;

namespace Query
{
    public class AuthorQueryIntegrationTest : ConfigureTest
    {
        protected Author Author;

        protected readonly IAuthorQuery _authorQuery;
        protected readonly Fixture _fixture;

        public AuthorQueryIntegrationTest()
        {
            _fixture = new();

            _authorQuery = new AuthorQuery(_dbConnection);
        }

        private Author CreateAuthor()
        {
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customize<Author>(c => c
              .With(x => x.Name, _fixture.Create<string>().PadRight(40, 'x').Substring(0, 40))
            );

            return _fixture.Create<Author>(); ;
        }


        [Fact]
        public async Task GetById_ShouldNotThrowExceptionAsync()
        {
            // Arrange                           
            Author = CreateAuthor();

            // Act
            var exception = await Record.ExceptionAsync(() => _authorQuery.Get(Author.Id));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetByFilter_ShouldNotThrowExceptionAsync()
        {
            // Arrange                           
            Author = CreateAuthor();

            // Act
            var exception = await Record.ExceptionAsync(() => _authorQuery.Get(Author.Name));

            // Assert
            Assert.Null(exception);
        }
    }
}