using BookManager.Application.UseCases.Book.GetById;

namespace BookManager.Application.UseCases.Book.GetByFilter
{
    public class GetByFilterResponse(IEnumerable<Domain.Entities.Book> books)
    {
        public IEnumerable<GetByIdResponse> Books { get; set; } = books.Select(book => new GetByIdResponse(book));
    }
}
