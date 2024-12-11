using Domain.Loans;

namespace Test.Data;

public static class LoanData
{
    public static Loan MainLoan => Loan.New(
        LoanId.New(),
        ReaderData.MainReader.Id,
        BookData.MainBook.Id,
        LibrarianData.MainLibrarian.Id,
        DateTime.UtcNow.AddDays(-5),
        DateTime.UtcNow.AddDays(10)
    );
}