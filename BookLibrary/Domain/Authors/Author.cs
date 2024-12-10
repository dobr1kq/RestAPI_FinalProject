namespace Domain.Authors;
public class Author
{
    public AuthorId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Country { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    private Author(AuthorId id, string firstName, string lastName, string country, DateTime dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Country = country;
        DateOfBirth = dateOfBirth;
    }
    
    public static Author New(AuthorId id, string firstName, string lastName, string country, DateTime dateOfBirth)
        => new Author(id, firstName, lastName, country, dateOfBirth);
    
    public void UpdateAuthorDetails(string firstName, string lastName, string country, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Country = country;
        DateOfBirth = dateOfBirth;
    }
}
