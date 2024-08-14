using AutoFixture;
using BookManager.Application.UseCases.Author.Delete;
using BookManager.Domain.Interfaces;
using Moq;

namespace AuthorTest
{
    public class DeleteAuthorTest
    {
        private readonly Mock<IBookRepository> _authorRepositoryMock;
        private readonly DeleteHandler _deleteHandler;
        private readonly Fixture _fixture;

        public DeleteAuthorTest()
        {
            _authorRepositoryMock = new();
            _deleteHandler = new(_authorRepositoryMock.Object);
            _fixture = new();
        }

        [Fact]
        public async Task ResponseWithErrors_WhenAuthorIsNotFound()
        {
            // Arrange
            int authorId = _fixture.Create<int>();
            var command = new DeleteCommand(authorId);

            _authorRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

            // Act
            var result = await _deleteHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorValidation);
            Assert.False(result.Found);

            _authorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Response_Success()
        {
            // Arrange
            int authorId = _fixture.Create<int>();
            var command = new DeleteCommand(authorId);

            _authorRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

            // Act
            var result = await _deleteHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorValidation);
            Assert.True(result.Found);

            _authorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}