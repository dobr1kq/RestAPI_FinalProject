namespace Domain.Librarians;

public class Librarian
{
    public LibrarianId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string TelephoneNumber { get; private set; }
    
}