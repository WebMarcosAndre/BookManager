using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Book.Update
{
    public class UpdateHandler(IBookRepository bookRepository) : IRequestHandler<UpdateCommand, ResultResponse>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        public async Task<ResultResponse> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var book = request.ToEntity();

            book.Validate();

            if (book.HasError())
            {
                return new ResultResponse(false) { ErrorValidation = book.ErrorValidation };
            }

            var result = await _bookRepository.UpdateAsync(book);

            await _bookRepository.DeleteAuthorBookAsync(book.Id);
            await _bookRepository.DeleteSubjectBookAsync(book.Id);

            await _bookRepository.InsertAuthorBookAsync(book);
            await _bookRepository.InsertSubjectBookAsync(book);

            return new ResultResponse(result);
        }
    }
}
