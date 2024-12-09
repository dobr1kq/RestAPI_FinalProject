namespace Domain.Authors;

public class Author
{
    public AuthorId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Country { get; private set; }
    public DateTime DateOfBirth { get; private set; }
}