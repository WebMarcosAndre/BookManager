using AutoFixture;
using BookManager.Application.UseCases.Author.Update;
using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using Moq;

namespace AuthorTest
{
    public class UpdateAuthorTest
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly UpdateHandler _updateHandler;
        private readonly Fixture _fixture;

        public UpdateAuthorTest()
        {
            _authorRepositoryMock = new();
            _updateHandler = new(_authorRepositoryMock.Object);
            _fixture = new();
        }

        [Fact]
        public async Task ResponseWithErrors_WhenAuthorIsNotFound()
        {
            // Arrange
            int authorId = _fixture.Create<int>();
            string authorName = _fixture.Create<string>();
            var command = new UpdateCommand(authorId, authorName);

            _authorRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Author>()))
            .ReturnsAsync(false);

            // Act
            var result = await _updateHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorValidation);
            Assert.False(result.Found);

            _authorRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Author>()), Times.Once);
        }

        [Fact]
        public async Task Response_Success()
        {
            // Arrange
            int authorId = _fixture.Create<int>();
            string authorName = _fixture.Create<string>();
            var command = new UpdateCommand(authorId, authorName);

            _authorRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Author>()))
            .ReturnsAsync(true);

            // Act
            var result = await _updateHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorValidation);
            Assert.True(result.Found);

            _authorRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Author>()), Times.Once);
        }
    }
}