using MediatR;

namespace BookManager.Application.UseCases.Author.GetByFilter
{
    public class GetByFilterQuery(string name) : IRequest<GetByFilterResponse>
    {
        public string Name { get; } = name;
    }
}
