using Domain.Books;
using Domain.Librarians;
using Domain.Readers;

namespace Domain.Loans;

public class Loan
{
    public LoanId Id { get; }
    public ReaderId ReaderId { get; }
    public BookId BookId { get; }
    public LibrarianId LibrarianId { get; }
    public DateTime LoanDate { get; private set; }
    public DateTime ReturnDate { get; private set; }
}