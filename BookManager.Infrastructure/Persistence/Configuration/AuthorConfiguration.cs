using BookManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.Infrastructure.Persistence.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Autor");

            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id)
                .HasColumnName("CodAu");

            builder.Property(p => p.Name)
               .HasColumnName("Nome")
               .HasColumnType("varchar")
               .HasMaxLength(40)
               .IsUnicode(false);

            builder.HasMany(p => p.Books)
                .WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "Livro_Autor",
                    book => book.HasOne<Book>()
                        .WithMany()
                        .HasForeignKey("Livro_CodL")
                        .HasConstraintName("Livro_Autor_FKIndex1")
                        .HasPrincipalKey(nameof(Book.Id)),
                    author => author.HasOne<Author>()
                        .WithMany()
                        .HasForeignKey("Autor_CodAu")
                        .HasConstraintName("Livro_Autor_FKIndex2")
                        .HasPrincipalKey(nameof(Author.Id)),
                    join =>
                    {
                        join.HasKey("Autor_CodAu", "Livro_CodL");
                        join.ToTable("Livro_Autor");
                        join.HasIndex("Livro_CodL", "Autor_CodAu")
                            .IsUnique(false)
                            .IsClustered(false)
                            .HasDatabaseName(null);
                    }
                );
        }
    }
}
