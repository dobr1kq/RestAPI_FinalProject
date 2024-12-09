namespace Domain.Loans;

public record LoanId(Guid Value)
{
    public static LoanId Empty() => new(Guid.Empty);
    public static LoanId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}