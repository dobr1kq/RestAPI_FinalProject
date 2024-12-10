namespace Domain.Librarians;

public class Librarian
{
    public LibrarianId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string TelephoneNumber { get; private set; }
    
    private Librarian(LibrarianId id, string firstName, string lastName, string telephoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        TelephoneNumber = telephoneNumber;
    }

    public static Librarian New(LibrarianId id, string firstName, string lastName, string telephoneNumber)
    {
        return new Librarian(id, firstName, lastName, telephoneNumber);
    }
    
    public void UpdateDetails(string firstName, string lastName, string telephoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        TelephoneNumber = telephoneNumber;
    }
}