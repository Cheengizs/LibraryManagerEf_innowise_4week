using Infrastructure.Constants;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.DbConfiguring;

public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> entity)
    {
        entity
            .HasKey(x => x.Id);

        entity
            .Property(a => a.Name)
            .HasMaxLength(AuthorConstants.BookTitleMaxLength)
            .IsRequired();
        
        entity
            .Property(a => a.DateOfBirth)
            .IsRequired();

        entity
            .HasMany(x => x.Books)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}