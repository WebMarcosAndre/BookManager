using BookManager.Domain.Entities;
using BookManager.Domain.Interfaces;
using Dapper;
using System.Data;

namespace BookManager.Infrastructure.Persistence.Repository.Query
{
    public class SubjectQuery(IDbConnection conn) : ISubjectQuery
    {
        private readonly IDbConnection _conn = conn;
        public async Task<Subject?> Get(int id)
        {
            return await _conn.QueryFirstOrDefaultAsync<Subject>("SELECT CodAs Id, Descricao Description FROM Assunto (NOLOCK) WHERE CodAs = @CodAs",
                new
                {
                    CodAs = id
                });
        }

        public async Task<IEnumerable<Subject>> Get(string description)
        {
            return await _conn.QueryAsync<Subject>("SELECT CodAs Id, Descricao Description FROM Assunto (NOLOCK) WHERE Descricao LIKE @Descricao",
                new
                {
                    Descricao = $"%{description}%"
                });
        }
    }
}
