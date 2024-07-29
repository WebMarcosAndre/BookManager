using MediatR;

namespace BookManager.Application.UseCases.Book.Update
{
    public class UpdateCommand(
        int id,
        string title,
        string publisherBook,
        int edition,
        string yearPublication,
        IEnumerable<int> authorIds,
        IEnumerable<int> subjectIds) : IRequest<ResultResponse>
    {
        public int Id { get; } = id;
        public string Title { get; } = title;
        public string PublisherBook { get; } = publisherBook;
        public int Edition { get; } = edition;
        public string YearPublication { get; } = yearPublication;
        public IEnumerable<int> AuthorIds { get; } = authorIds ?? [];
        public IEnumerable<int> SubjectIds { get; } = subjectIds ?? [];
        public Domain.Entities.Book ToEntity()
        {
            return new Domain.Entities.Book
            {
                Id = Id,
                Title = Title,
                PublisherBook = PublisherBook,
                Edition = Edition,
                YearPublication = YearPublication,
                Authors = AuthorIds.Select(authorId => new Domain.Entities.Author() { Id = authorId }).ToList(),
                Subjects = SubjectIds.Select(subjectId => new Domain.Entities.Subject() { Id = subjectId }).ToList()
            };
        }
    }
}
