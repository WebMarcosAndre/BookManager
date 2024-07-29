namespace BookManager.Application.UseCases.Book.Create
{
    public class CreateResponse(Domain.Entities.Book book) : ResponseBase
    {
        public int Id { get; } = book.Id;
        public string Title { get; } = book.Title;
        public string PublisherBook { get; } = book.PublisherBook;
        public int Edition { get; } = book.Edition;
        public string YearPublication { get; } = book.YearPublication;
    }
}
