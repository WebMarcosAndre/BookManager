using BookManager.Domain.Interfaces;
using BookManager.Infrastructure.Persistence.Repository.Command;
using BookManager.Infrastructure.Persistence.Repository.Query;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace BookManager.Infrastructure.Persistence
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("ConnectionString");
            var connectionStringReadonly = configuration.GetConnectionString("ConnectionStringReadonly");

            services.AddDbContext<BookManangerDbContext>(
                options => options.UseSqlServer(connectionString,
                migration => migration.MigrationsAssembly("BookManager.Infrastructure")));

            services.AddScoped<IDbConnection>(sp =>
            {
                return new SqlConnection(connectionString);
            });

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorQuery, AuthorQuery>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookQuery, BookQuery>();

            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISubjectQuery, SubjectQuery>();

            return services;
        }
    }
}