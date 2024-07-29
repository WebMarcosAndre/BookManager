using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Author.GetById
{
    public class GetByIdHandler(IAuthorQuery authorQuery) : IRequestHandler<GetByIdQuery, GetByIdResponse>
    {
        private readonly IAuthorQuery _authorQuery = authorQuery;

        public async Task<GetByIdResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorQuery.Get(request.Id);

            if (author == null)
            {
                return null;
            }

            var response = new GetByIdResponse(author);

            return response;
        }
    }
}
