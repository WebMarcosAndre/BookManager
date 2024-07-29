using MediatR;

namespace BookManager.Application.UseCases.Author.Update
{
    public class UpdateCommand(int id, string name) : IRequest<ResultResponse>
    {
        public int Id { get; } = id;
        public string Name { get; } = name;

        public Domain.Entities.Author ToEntity()
        {
            return new Domain.Entities.Author
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
