using MediatR;

namespace BookManager.Application.UseCases.Author.Create
{
    public class CreateCommand(string name) : IRequest<CreateResponse>
    {
        public string Name { get; } = name;

        public virtual Domain.Entities.Author ToEntity()
        {
            return new Domain.Entities.Author
            {
                Name = Name
            };
        }
    }
}

