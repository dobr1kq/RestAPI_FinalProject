using Domain.Books;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new BookId(x))
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(x => x.Date)
            .IsRequired()
            .HasConversion(new DateTimeUtcConverter());
    }
}
