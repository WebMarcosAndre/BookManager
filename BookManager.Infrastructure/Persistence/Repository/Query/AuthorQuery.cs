using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using Dapper;
using System.Data;

namespace BookManager.Infrastructure.Persistence.Repository.Query
{
    public class AuthorQuery(IDbConnection conn) : IAuthorQuery
    {
        private readonly IDbConnection _conn = conn;
        public async Task<Author?> Get(int id)
        {
            return await _conn.QueryFirstOrDefaultAsync<Author>("SELECT CodAu Id, Nome Name FROM Autor (NOLOCK) WHERE CodAu = @CodAu",
                new
                {
                    CodAu = id
                });
        }

        public async Task<IEnumerable<Author>> Get(string name)
        {
            return await _conn.QueryAsync<Author>("SELECT CodAu Id, Nome Name FROM Autor (NOLOCK) WHERE Nome LIKE @Nome ORDER BY Nome",
                new
                {
                    Nome = $"%{name}%"
                });
        }
    }
}
