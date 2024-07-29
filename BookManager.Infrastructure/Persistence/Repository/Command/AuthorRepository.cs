using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using Dapper;
using System.Data;

namespace BookManager.Infrastructure.Persistence.Repository.Command
{
    public class AuthorRepository(IDbConnection conn) : IAuthorRepository
    {
        private readonly IDbConnection _conn = conn;

        public async Task InsertAsync(Author author)
        {
            author.Id = await _conn.QuerySingleAsync<int>("INSERT INTO Autor (Nome) OUTPUT INSERTED.CodAu VALUES (@Nome)",
                new
                {
                    Nome = author.Name
                });
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            return await _conn.ExecuteAsync("UPDATE Autor SET Nome = @Nome WHERE CodAu = @CodAu",
                new
                {
                    CodAu = author.Id,
                    Nome = author.Name
                }) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _conn.ExecuteAsync("DELETE FROM Autor WHERE CodAu = @CodAu",
              new
              {
                  CodAu = id
              }) > 0;
        }
    }
}
