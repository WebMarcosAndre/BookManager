using BookManager.Domain.Entities;

namespace BookManager.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        Task InsertAsync(Subject subject);
        Task<bool> UpdateAsync(Subject subject);
        Task<bool> DeleteAsync(int id);
    }
}
