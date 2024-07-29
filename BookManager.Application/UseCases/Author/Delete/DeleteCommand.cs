using MediatR;

namespace BookManager.Application.UseCases.Author.Delete
{
    public class DeleteCommand(int id) : IRequest<ResultResponse>
    {
        public int Id { get; } = id;
    }
}
