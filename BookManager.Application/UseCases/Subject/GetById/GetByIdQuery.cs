using MediatR;

namespace BookManager.Application.UseCases.Subject.GetById
{
    public class GetByIdQuery(int id) : IRequest<GetByIdResponse>
    {
        public int Id { get; } = id;
    }
}
