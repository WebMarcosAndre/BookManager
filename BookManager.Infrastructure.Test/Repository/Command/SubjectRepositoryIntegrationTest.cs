using AutoFixture;
using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using BookManager.Infrastructure.Persistence.Repository.Command;
using BookManager.Infrastructure.Persistence.Repository.Query;
using BookManager.Infrastructure.Test.Repository;

namespace Command
{
    public class SubjectRepositoryIntegrationTest : ConfigureTest
    {
        protected Subject Subject;
        protected readonly ISubjectRepository _subjectRepository;
        protected readonly ISubjectQuery _subjectQuery;
        protected readonly Fixture _fixture;

        public SubjectRepositoryIntegrationTest()
        {
            _fixture = new();

            _subjectRepository = new SubjectRepository(_dbConnection);
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
        public async Task Insert_ShouldInsertAsync()
        {
            // Arrange                              
            Subject = CreateSubject();

            // Act
            var exception = await Record.ExceptionAsync(() => _subjectRepository.InsertAsync(Subject));
            var insertedSubject = await _subjectQuery.Get(Subject.Id);
            await _subjectRepository.DeleteAsync(Subject.Id);

            // Assert
            Assert.Null(exception);
            Assert.NotNull(insertedSubject);
            Assert.Equal(Subject.Description, insertedSubject.Description);
        }

        [Fact]
        public async Task Update_ShouldUpdateAsync()
        {
            // Arrange
            Subject = CreateSubject();

            await _subjectRepository.InsertAsync(Subject);

            var updateSubject = CreateSubject();

            updateSubject.Id = Subject.Id;

            // Act
            var exception = await Record.ExceptionAsync(() => _subjectRepository.UpdateAsync(updateSubject));
            var updatedSubject = await _subjectQuery.Get(Subject.Id);
            await _subjectRepository.DeleteAsync(Subject.Id);

            // Assert                          
            Assert.Null(exception);
            Assert.NotNull(updatedSubject);
            Assert.NotEqual(Subject.Description, updateSubject.Description);
            Assert.Equal(updateSubject.Description, updatedSubject.Description);
        }

        [Fact]
        public async Task Delete_ShouldDeleteAuthorAsync()
        {
            // Arrange  
            Subject = CreateSubject();

            await _subjectRepository.InsertAsync(Subject);

            // Act
            var exception = await Record.ExceptionAsync(() => _subjectRepository.DeleteAsync(Subject.Id));
            var deletedSubject = await _subjectQuery.Get(Subject.Id);

            // Assert
            Assert.Null(exception);
            Assert.Null(deletedSubject);
        }
    }
}