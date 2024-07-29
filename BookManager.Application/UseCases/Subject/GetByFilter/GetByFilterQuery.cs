using MediatR;

namespace BookManager.Application.UseCases.Subject.GetByName
{
    public class GetByFilterQuery(string description) : IRequest<GetByFilterResponse>
    {
        public string Description { get; } = description;
    }
}
