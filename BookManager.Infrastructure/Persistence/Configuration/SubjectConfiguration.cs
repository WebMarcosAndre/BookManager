using BookManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.Infrastructure.Persistence.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Assunto");

            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id)
                .HasColumnName("codAs");

            builder.Property(p => p.Description)
                .HasColumnName("Descricao")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsUnicode(false);

        }
    }
}
