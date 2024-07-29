using BookManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.Infrastructure.Persistence.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Livro");

            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id)
                .HasColumnName("CodL");

            builder.Property(p => p.Title)
                .HasColumnName("Titulo")
                .HasColumnType("varchar")
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(p => p.PublisherBook)
                .HasColumnName("Editora")
                .HasColumnType("varchar")
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(p => p.Edition)
                .HasColumnName("Edicao");

            builder.Property(p => p.YearPublication)
                .HasColumnName("AnoPublicacao")
                .HasColumnType("varchar")
                .HasMaxLength(4)
                .IsUnicode(false);

            builder.HasMany(p => p.Subjects)
                .WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                      "Livro_Assunto",
                      book => book.HasOne<Subject>()
                          .WithMany()
                          .HasForeignKey("Assunto_CodAs")
                          .HasConstraintName("Livro_Assunto_FKIndex1")
                          .HasPrincipalKey(nameof(Subject.Id)),
                      author => author.HasOne<Book>()
                          .WithMany()
                          .HasForeignKey("Livro_CodL")
                          .HasConstraintName("Livro_Assunto_FKIndex2")
                          .HasPrincipalKey(nameof(Book.Id)),
                      join =>
                      {
                          join.HasKey("Assunto_CodAs", "Livro_CodL");
                          join.ToTable("Livro_Assunto");
                          join.HasIndex("Livro_CodL", "Assunto_CodAs")
                              .IsUnique(false)
                              .IsClustered(false)
                              .HasDatabaseName(null);
                      }
                );
        }
    }
}
