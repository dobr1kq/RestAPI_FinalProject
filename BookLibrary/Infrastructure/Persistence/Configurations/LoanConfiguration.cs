using Domain.Books;
using Domain.Librarians;
using Domain.Loans;
using Domain.Readers;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new LoanId(x))
            .IsRequired();

        builder.Property(x => x.ReaderId)
            .HasConversion(x => x.Value, x => new ReaderId(x))
            .IsRequired();

        builder.Property(x => x.BookId)
            .HasConversion(x => x.Value, x => new BookId(x))
            .IsRequired();

        builder.Property(x => x.LibrarianId)
            .HasConversion(x => x.Value, x => new LibrarianId(x))
            .IsRequired();

        builder.Property(x => x.LoanDate)
            .IsRequired()
            .HasConversion(new DateTimeUtcConverter());

        builder.Property(x => x.ReturnDate)
            .IsRequired()
            .HasConversion(new DateTimeUtcConverter());
    }
}
