using Domain.Authors;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AuthorId(x))
            .IsRequired();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(x => x.Country)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(x => x.DateOfBirth)
            .IsRequired()
            .HasConversion(new DateTimeUtcConverter());
    }
}
