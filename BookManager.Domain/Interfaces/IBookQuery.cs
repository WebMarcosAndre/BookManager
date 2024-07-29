using BookManager.Domain.Entities;

namespace BookManager.Domain.Interfaces
{
    public interface IBookQuery
    {
        Task<Book> Get(int id);
        Task<IEnumerable<Book>> Get(Book book);
    }
}
