using MediatR;

namespace BookManager.Application.UseCases.Book.Create
{
    public class CreateCommand(
        string title,
        string publisherBook,
        int edition,
        string yearPublication,
        IEnumerable<int> authorIds,
        IEnumerable<int> subjectIds) : IRequest<CreateResponse>
    {
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
