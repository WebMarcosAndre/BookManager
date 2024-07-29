using BookManager.Domain.Entities;

namespace BookManager.Domain.Interfaces
{
    public interface IAuthorQuery
    {
        Task<Author> Get(int id);
        Task<IEnumerable<Author>> Get(string name);
    }
}
