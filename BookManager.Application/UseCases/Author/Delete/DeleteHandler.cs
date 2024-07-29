using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Author.Delete
{
    public class DeleteHandler(IBookRepository authorRepository) : IRequestHandler<DeleteCommand, ResultResponse>
    {
        private readonly IBookRepository _authorRepository = authorRepository;
        public async Task<ResultResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorRepository.DeleteAsync(request.Id);
            return new ResultResponse(result);
        }
    }
}
