using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Book.Create
{
    public class CreateHandler(IBookRepository bookRepository) : IRequestHandler<CreateCommand, CreateResponse>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        public async Task<CreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var book = request.ToEntity();

            book.Validate();

            if (book.HasError())
            {
                return new CreateResponse(book) { ErrorValidation = book.ErrorValidation };
            }

            await _bookRepository.InsertAsync(book);
            await _bookRepository.InsertAuthorBookAsync(book);
            await _bookRepository.InsertSubjectBookAsync(book);

            return new CreateResponse(book);
        }
    }
}
