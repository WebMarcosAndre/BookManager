namespace BookManager.Application.UseCases.Subject.Create
{
    public class CreateResponse(Domain.Entities.Subject subject) : ResponseBase
    {
        public int Id { get; } = subject.Id;
        public string Description { get; } = subject.Description;
    }
}
