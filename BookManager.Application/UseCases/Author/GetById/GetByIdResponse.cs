
namespace BookManager.Application.UseCases.Author.GetById
{
    public class GetByIdResponse(Domain.Entities.Author author)
    {
        public int Id { get; } = author.Id;
        public string Name { get; } = author.Name;

    }
}
