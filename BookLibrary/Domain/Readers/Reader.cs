namespace Domain.Readers;

public class Reader
{
    public ReaderId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string TelephoneNumber { get; private set; }
    
}