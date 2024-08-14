using AutoFixture;
using BookManager.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookManager.Domain.Test
{
    public class StructureValidationTest
    {
        private Fixture _fixture;

        public StructureValidationTest()
        {
            _fixture = new();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void Author_ShouldFailure()
        {
            //Arrange
            _fixture.Customize<Author>(c => c
               .With(x => x.Name, _fixture.Create<string>().PadRight(100, 'x').Substring(0, 100))
           );
            var author = _fixture.Create<Author>();

            var errorExpected = new StringLengthRangeAttribute("Nome", 5, 40).FormatErrorMessage("");

            //Act
            author.Validate();

            //Assert
            Assert.NotNull(author.ErrorValidation);
            Assert.NotEmpty(author.ErrorValidation.ErrorMessages);
            Assert.Single(author.ErrorValidation.ErrorMessages);
            Assert.Contains(errorExpected, author.ErrorValidation.ErrorMessages);
        }

        [Fact]
        public void Subject_ShouldFailure()
        {
            //Arrange
            _fixture.Customize<Subject>(c => c
               .With(x => x.Description, _fixture.Create<string>().PadRight(100, 'x').Substring(0, 100))
           );
            var author = _fixture.Create<Subject>();

            var errorExpected = new StringLengthRangeAttribute("Descrição", 5, 20).FormatErrorMessage("");

            //Act
            author.Validate();

            //Assert
            Assert.NotNull(author.ErrorValidation);
            Assert.NotEmpty(author.ErrorValidation.ErrorMessages);
            Assert.Single(author.ErrorValidation.ErrorMessages);
            Assert.Contains(errorExpected, author.ErrorValidation.ErrorMessages);
        }

        [Fact]
        public void Book_ShouldFailure()
        {
            //Arrange
            _fixture.Customize<Book>(c => c
               .With(x => x.Title, _fixture.Create<string>().PadRight(100, 'x').Substring(0, 100))
               .With(x => x.PublisherBook, _fixture.Create<string>().PadRight(100, 'x').Substring(0, 100))
               .With(x => x.Edition, 0)
               .With(x => x.YearPublication, (DateTime.Now.Year + 1).ToString())
           );
            var book = _fixture.Create<Book>();

            var errorsExpected = new List<string> {
                new StringLengthRangeAttribute("Título", 5, 40).FormatErrorMessage(""),
                new StringLengthRangeAttribute("Editora", 5, 40).FormatErrorMessage(""),
                new RangeAttribute(1, int.MaxValue) { ErrorMessage = "Edição deve ser maior que zero (0)." }.FormatErrorMessage("1"),
                new YearLessThanOrEqualToCurrentYearAttribute("ano de publicação").FormatErrorMessage("")
            };

            //Act
            book.Validate();

            //Assert
            Assert.NotNull(book.ErrorValidation);
            Assert.NotEmpty(book.ErrorValidation.ErrorMessages);
            Assert.Equal(errorsExpected.Count, book.ErrorValidation.ErrorMessages.Count());
            Assert.Equal(
                errorsExpected.OrderBy(errorExpected => errorExpected),
                book.ErrorValidation.ErrorMessages.OrderBy(errorExpected => errorExpected));
        }

        [Fact]
        public void Book_ShouldFailureRequired()
        {
            //Arrange            
            var book = new Book();

            var errorsExpected = new List<string> {
                new RequiredAttribute{ErrorMessage="Título é obrigatório."}.FormatErrorMessage(""),
                new RequiredAttribute{ErrorMessage="Editora é obrigatório."}.FormatErrorMessage(""),
                new RangeAttribute(1, int.MaxValue) { ErrorMessage = "Edição deve ser maior que zero (0)." }.FormatErrorMessage("1"),
                new RequiredAttribute{ErrorMessage="Ano de publicação é obrigatório."}.FormatErrorMessage("")
            };

            //Act
            book.Validate();

            //Assert
            Assert.NotNull(book.ErrorValidation);
            Assert.NotEmpty(book.ErrorValidation.ErrorMessages);
            Assert.Equal(errorsExpected.Count, book.ErrorValidation.ErrorMessages.Count());
            Assert.Equal(
                errorsExpected.OrderBy(errorExpected => errorExpected),
                book.ErrorValidation.ErrorMessages.OrderBy(errorExpected => errorExpected));
        }
    }
}