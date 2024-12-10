namespace Domain.Readers;

public class Reader
{
    public ReaderId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string TelephoneNumber { get; private set; }
    
    private Reader(ReaderId id, string firstName, string lastName, string telephoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        TelephoneNumber = telephoneNumber;
    }

    public static Reader Create(ReaderId id, string firstName, string lastName, string telephoneNumber)
    {
        return new Reader(id, firstName, lastName, telephoneNumber);
    }
    
    public void UpdateDetails(string firstName, string lastName, string telephoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        TelephoneNumber = telephoneNumber;
    }
}