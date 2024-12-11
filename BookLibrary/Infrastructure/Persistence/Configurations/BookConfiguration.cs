using Domain.Authors;
using Domain.Books;
using Domain.Genres;
using Domain.Publishers;
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

        builder.Property(x => x.GenreId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new GenreId(x));

        builder.Property(x => x.AuthorId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new AuthorId(x));
        
        builder.Property(x => x.PublisherId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new PublisherId(x));
    }
}

