using MediatR;

namespace BookManager.Application.UseCases.Author.GetById
{
    public class GetByIdQuery(int id) : IRequest<GetByIdResponse>
    {
        public int Id { get; } = id;
    }
}
