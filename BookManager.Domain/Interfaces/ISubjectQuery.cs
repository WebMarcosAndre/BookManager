using BookManager.Domain.Entities;

namespace BookManager.Domain.Interfaces
{
    public interface ISubjectQuery
    {
        Task<Subject> Get(int id);
        Task<IEnumerable<Subject>> Get(string description);
    }
}
