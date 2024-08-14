using AutoFixture;
using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using BookManager.Infrastructure.Persistence.Repository.Query;
using BookManager.Infrastructure.Test.Repository;

namespace Query
{
    public class SubjectQueryIntegrationTest : ConfigureTest
    {
        protected Subject Subject;

        protected readonly ISubjectQuery _subjectQuery;
        protected readonly Fixture _fixture;

        public SubjectQueryIntegrationTest()
        {
            _fixture = new();

            _subjectQuery = new SubjectQuery(_dbConnection);
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
        public async Task GetById_ShouldNotThrowExceptionAsync()
        {
            // Arrange                           
            Subject = CreateSubject();

            // Act
            var exception = await Record.ExceptionAsync(() => _subjectQuery.Get(Subject.Id));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetByFilter_ShouldNotThrowExceptionAsync()
        {
            // Arrange                           
            Subject = CreateSubject();

            // Act
            var exception = await Record.ExceptionAsync(() => _subjectQuery.Get(Subject.Description));

            // Assert
            Assert.Null(exception);
        }
    }
}