using AutoFixture;
using BookManager.Application.UseCases.Author.Create;
using BookManager.Domain.Interfaces;
using Moq;
using Author = BookManager.Domain.Entities.Author;

namespace AuthorTest
{
    public class CreateHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly CreateHandler _createHandler;
        private readonly Fixture _fixture;

        public CreateHandlerTests()
        {
            _authorRepositoryMock = new();
            _createHandler = new(_authorRepositoryMock.Object);
            _fixture = new();
        }

        [Fact]
        public async Task ResponseWithErrors_WhenAuthorIsInvalid()
        {
            // Arrange
            string invalidName = _fixture.Create<string>().ToString().Substring(0, 3);
            var command = new CreateCommand(invalidName);

            var errorExpected = new StringLengthRangeAttribute("Nome", 5, 40).FormatErrorMessage("");

            // Act
            var result = await _createHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ErrorValidation);
            Assert.Single(result.ErrorValidation.ErrorMessages);
            Assert.Contains(errorExpected, result.ErrorValidation.ErrorMessages);

            _authorRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Response_Success()
        {
            // Arrange
            var command = _fixture.Create<CreateCommand>();

            _authorRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _createHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ErrorValidation);
            _authorRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<Author>()), Times.Once);
        }
    }
}