using BookManager.Domain.Entities;

namespace BookManager.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task InsertAsync(Author author);
        Task<bool> UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
    }
}
