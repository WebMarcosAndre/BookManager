using MediatR;

namespace BookManager.Application.UseCases.Subject.Create
{
    public class CreateCommand(string description) : IRequest<CreateResponse>
    {
        public string Description { get; } = description;

        public Domain.Entities.Subject ToEntity()
        {
            return new Domain.Entities.Subject
            {
                Description = Description
            };
        }
    }
}
