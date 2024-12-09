using Domain.Publishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new PublisherId(x))
            .IsRequired();

        builder.Property(x => x.PublisherName)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(x => x.PublisherAddress)
            .IsRequired()
            .HasColumnType("varchar(500)");
    }
}
