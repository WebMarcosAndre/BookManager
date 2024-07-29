using MediatR;

namespace BookManager.Application.UseCases.Subject.Delete
{
    public class DeleteCommand(int id) : IRequest<ResultResponse>
    {
        public int Id { get; } = id;
    }
}
