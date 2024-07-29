using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using Dapper;
using System.Data;

namespace BookManager.Infrastructure.Persistence.Repository.Command
{
    public class SubjectRepository(IDbConnection conn) : ISubjectRepository
    {
        private readonly IDbConnection _conn = conn;

        public async Task InsertAsync(Subject subject)
        {
            subject.Id = await _conn.QuerySingleAsync<int>("INSERT INTO Assunto (Descricao) OUTPUT INSERTED.CodAs  VALUES (@Descricao)",
                new
                {
                    Descricao = subject.Description
                });
        }

        public async Task<bool> UpdateAsync(Subject subject)
        {
            return await _conn.ExecuteAsync("UPDATE Assunto SET Descricao = @Descricao WHERE CodAs = @CodAs ",
                new
                {
                    CodAs = subject.Id,
                    Descricao = subject.Description
                }) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _conn.ExecuteAsync("DELETE FROM Assunto WHERE CodAs  = @CodAs ",
              new
              {
                  CodAs = id
              }) > 0;
        }
    }
}
