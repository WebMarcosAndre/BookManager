using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Author.Update
{
    public class UpdateHandler(IAuthorRepository authorRepository) : IRequestHandler<UpdateCommand, ResultResponse>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        public async Task<ResultResponse> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var author = request.ToEntity();

            author.Validate();

            if (author.HasError())
            {
                return new ResultResponse(false) { ErrorValidation = author.ErrorValidation };
            }

            var result = await _authorRepository.UpdateAsync(author);

            return new ResultResponse(result);
        }
    }
}
