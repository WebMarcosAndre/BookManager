using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.Domain.Validators
{
    public class EntityValidate
    {
        [NotMapped]
        public ErrorValidation ErrorValidation { get; private set; } = new();

        public IEnumerable<string> Validate()
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(this);

            Validator.TryValidateObject(this, context, validationResults, true);

            ErrorValidation.ErrorMessages = validationResults.Select(vr => vr.ErrorMessage);

            return ErrorValidation.ErrorMessages;
        }

        public bool HasError()
        {
            Validate();

            return ErrorValidation.ErrorMessages.Any();
        }
    }
}