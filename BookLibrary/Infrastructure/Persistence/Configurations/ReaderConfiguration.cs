using Domain.Readers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
{
    public void Configure(EntityTypeBuilder<Reader> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ReaderId(x))
            .IsRequired();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(x => x.TelephoneNumber)
            .IsRequired()
            .HasColumnType("varchar(15)");
    }
}
