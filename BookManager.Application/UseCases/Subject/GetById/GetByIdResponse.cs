namespace BookManager.Application.UseCases.Subject.GetById
{
    public class GetByIdResponse(Domain.Entities.Subject subject)
    {
        public int Id { get; } = subject.Id;
        public string Description { get; } = subject.Description;

    }
}
