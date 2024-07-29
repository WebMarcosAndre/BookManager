namespace BookManager.Application.UseCases.Author.Create
{
    public class CreateResponse(Domain.Entities.Author author) : ResponseBase
    {
        public int Id { get; } = author.Id;
        public string Name { get; } = author.Name;
    }
}
