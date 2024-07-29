using MediatR;

namespace BookManager.Application.UseCases.Book.GetByFilter
{
    public class GetByFilterQuery(string title, string publisherBook, int? edition, string yearPublication) : IRequest<GetByFilterResponse>
    {
        public string Title { get; } = title;
        public string PublisherBook { get; } = publisherBook;
        public int? Edition { get; } = edition;
        public string YearPublication { get; } = yearPublication;

        internal Domain.Entities.Book ToEntity()
        {
            return new Domain.Entities.Book
            {
                Title = Title,
                PublisherBook = PublisherBook,
                Edition = Edition ?? 0,
                YearPublication = YearPublication
            };
        }
    }
}
