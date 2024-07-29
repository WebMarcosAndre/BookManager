using MediatR;

namespace BookManager.Application.UseCases.Book.Delete
{
    public class DeleteCommand(int id) : IRequest<ResultResponse>
    {
        public int Id { get; } = id;
    }
}
