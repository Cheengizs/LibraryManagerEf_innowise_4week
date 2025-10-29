using Infrastructure.Constants;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfiguring;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> entity) 
    {
        entity.HasKey(x => x.Id);

        entity
            .HasOne(x => x.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(x => x.AuthorId);
        
        entity
            .Property(b => b.PublishedYear)
            .IsRequired();

        entity.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Book_PublisherYear_MinValue",
                $"[PublisherYear] >= {BookConstants.BookMinYear}");
        });
        
        entity
            .Property(o => o.Title)
            .HasMaxLength(BookConstants.BookTitleMaxLength)
            .IsRequired();
    }
}