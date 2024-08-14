using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Author.GetByFilter
{
    public class GetByFilterHandler(IAuthorQuery authorQuery) : IRequestHandler<GetByFilterQuery, GetByFilterResponse>
    {
        private readonly IAuthorQuery _authorQuery = authorQuery;

        public async Task<GetByFilterResponse> Handle(GetByFilterQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorQuery.Get(request.Name);

            var response = new GetByFilterResponse(authors);

            return response;
        }
    }
}
