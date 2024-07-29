using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Book.GetByFilter
{
    public class GetByFilterHandler(IBookQuery bookQuery) : IRequestHandler<GetByFilterQuery, GetByFilterResponse>
    {
        private readonly IBookQuery _bookQuery = bookQuery;

        public async Task<GetByFilterResponse> Handle(GetByFilterQuery request, CancellationToken cancellationToken)
        {
            var book = request.ToEntity();
            var books = await _bookQuery.Get(book);

            var response = new GetByFilterResponse(books);

            return response;
        }
    }
}
