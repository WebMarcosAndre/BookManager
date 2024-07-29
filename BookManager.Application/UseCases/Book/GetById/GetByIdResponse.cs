namespace BookManager.Application.UseCases.Book.GetById
{
    public class GetByIdResponse(Domain.Entities.Book book)
    {
        public int Id { get; } = book.Id;
        public string Title { get; } = book.Title;
        public string PublisherBook { get; } = book.PublisherBook;
        public int Edition { get; } = book.Edition;
        public string YearPublication { get; } = book.YearPublication;
        public IEnumerable<Author.GetById.GetByIdResponse> Authors { get; } = book.Authors.Select(author => new Author.GetById.GetByIdResponse(author));
        public IEnumerable<Subject.GetById.GetByIdResponse> Subjects { get; } = book.Subjects.Select(subject => new Subject.GetById.GetByIdResponse(subject));
    }
}
