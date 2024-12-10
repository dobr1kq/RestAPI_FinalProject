using Domain.Books;
using Domain.Librarians;
using Domain.Readers;

namespace Domain.Loans;

public class Loan
{
    public LoanId Id { get; }
    public ReaderId ReaderId { get; private set; }
    public BookId BookId { get; private set; }
    public LibrarianId LibrarianId { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime ReturnDate { get; private set; }
    
    private Loan(LoanId id, ReaderId readerId, BookId bookId, LibrarianId librarianId, DateTime loanDate, DateTime returnDate)
    {
        Id = id;
        ReaderId = readerId;
        BookId = bookId;
        LibrarianId = librarianId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
    }

    public static Loan Create(LoanId id, ReaderId readerId, BookId bookId, LibrarianId librarianId, DateTime loanDate, DateTime returnDate)
    {
        return new Loan(id, readerId, bookId, librarianId, loanDate, returnDate);
    }
    
    public void UpdateDetails(DateTime loanDate, DateTime returnDate, ReaderId readerId, BookId bookId, LibrarianId librarianId)
    {
        ReaderId = readerId;
        BookId = bookId;
        LibrarianId = librarianId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
    }
}