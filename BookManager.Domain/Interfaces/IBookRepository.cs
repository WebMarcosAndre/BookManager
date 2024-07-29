using BookManager.Domain.Entities;

namespace BookManager.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task InsertAsync(Book book);
        Task InsertAuthorBookAsync(Book book);
        Task InsertSubjectBookAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task DeleteAuthorBookAsync(int id);
        Task DeleteSubjectBookAsync(int id);
    }
}
