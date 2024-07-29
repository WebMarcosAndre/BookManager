using BookManager.Application.UseCases.Author.GetById;

namespace BookManager.Application.UseCases.Author.GetByName
{
    public class GetByFilterResponse(IEnumerable<Domain.Entities.Author> authors)
    {
        public IEnumerable<GetByIdResponse> Authors { get; set; } = authors.Select(author => new GetByIdResponse(author));
    }
}
