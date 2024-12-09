using Domain.Librarians;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LibrarianConfiguration : IEntityTypeConfiguration<Librarian>
{
    public void Configure(EntityTypeBuilder<Librarian> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new LibrarianId(x))
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
