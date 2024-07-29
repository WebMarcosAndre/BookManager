using BookManager.Domain.Validators;

namespace BookManager.Application.UseCases
{
    public class ResponseBase
    {
        public ErrorValidation ErrorValidation { get; set; }

        public bool HasError()
        {
            return ErrorValidation?.ErrorMessages?.Any() ?? false;
        }
    }
}
