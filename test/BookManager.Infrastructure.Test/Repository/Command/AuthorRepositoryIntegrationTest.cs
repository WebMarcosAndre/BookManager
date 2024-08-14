using AutoFixture;
using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using BookManager.Infrastructure.Persistence.Repository.Command;
using BookManager.Infrastructure.Persistence.Repository.Query;
using BookManager.Infrastructure.Test.Repository;

namespace Command
{
    public class AuthorRepositoryIntegrationTest : ConfigureTest
    {
        protected Author Author;
        protected readonly IAuthorRepository _authorRepository;
        protected readonly IAuthorQuery _authorQuery;
        protected readonly Fixture _fixture;

        public AuthorRepositoryIntegrationTest()
        {
            _fixture = new();

            _authorRepository = new AuthorRepository(_dbConnection);
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
        public async Task Insert_ShouldInsertAsync()
        {
            // Arrange                           
            Author = CreateAuthor();

            // Act
            var exception = await Record.ExceptionAsync(() => _authorRepository.InsertAsync(Author));
            var insertedAuthor = await _authorQuery.Get(Author.Id);
            await _authorRepository.DeleteAsync(Author.Id);

            // Assert
            Assert.Null(exception);
            Assert.NotNull(insertedAuthor);
            Assert.Equal(Author.Name, insertedAuthor.Name);
        }

        [Fact]
        public async Task Update_ShouldUpdateAsync()
        {
            // Arrange                         
            Author = CreateAuthor();

            await _authorRepository.InsertAsync(Author);

            var updateAuthor = CreateAuthor();
            updateAuthor.Id = Author.Id;

            // Act
            var exception = await Record.ExceptionAsync(() => _authorRepository.UpdateAsync(updateAuthor));
            var updated = await _authorQuery.Get(Author.Id);
            await _authorRepository.DeleteAsync(Author.Id);

            // Assert                          
            Assert.Null(exception);
            Assert.NotNull(updated);
            Assert.NotEqual(Author.Name, updateAuthor.Name);
            Assert.Equal(updateAuthor.Name, updated.Name);
        }

        [Fact]
        public async Task Delete_ShouldDeleteAsync()
        {
            // Arrange             
            Author = CreateAuthor();

            await _authorRepository.InsertAsync(Author);

            // Act
            var exception = await Record.ExceptionAsync(() => _authorRepository.DeleteAsync(Author.Id));
            var deletedAuthor = await _authorQuery.Get(Author.Id);

            // Assert
            Assert.Null(exception);
            Assert.Null(deletedAuthor);
        }
    }
}