using MediatR;

namespace BookManager.Application.UseCases.Author.GetByName
{
    public class GetByFilterQuery(string name) : IRequest<GetByFilterResponse>
    {
        public string Name { get; } = name;
    }
}
