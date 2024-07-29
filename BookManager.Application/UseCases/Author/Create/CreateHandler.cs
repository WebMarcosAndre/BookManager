using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Author.Create
{
    public class CreateHandler(IAuthorRepository authorRepository) : IRequestHandler<CreateCommand, CreateResponse>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        public async Task<CreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var author = request.ToEntity();

            author.Validate();

            if (author.HasError())
            {
                return new CreateResponse(author) { ErrorValidation = author.ErrorValidation };
            }

            await _authorRepository.InsertAsync(author);

            return new CreateResponse(author);
        }
    }
}
