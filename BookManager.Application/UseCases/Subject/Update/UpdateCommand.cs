using MediatR;

namespace BookManager.Application.UseCases.Subject.Update
{
    public class UpdateCommand(int id, string description) : IRequest<ResultResponse>
    {
        public int Id { get; } = id;
        public string Description { get; } = description;
        public Domain.Entities.Subject ToEntity()
        {
            return new Domain.Entities.Subject
            {
                Id = Id,
                Description = Description
            };
        }
    }
}
