using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Book.GetById
{
    public class GetByIdHandler(IBookQuery bookQuery) : IRequestHandler<GetByIdQuery, GetByIdResponse>
    {
        private readonly IBookQuery _bookQuery = bookQuery;

        public async Task<GetByIdResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookQuery.Get(request.Id);

            if (book == null)
            {
                return null;
            }

            var response = new GetByIdResponse(book);

            return response;
        }
    }
}
