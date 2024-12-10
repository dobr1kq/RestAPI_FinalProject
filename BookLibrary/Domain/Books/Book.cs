namespace Domain.Books;

public class Book
{
    public BookId Id { get; }
    public string Name {get; private set;} 
    public DateTime Date {get; private set;}
    
    private Book(BookId id, string name, DateTime date)
    {
        Id = id;
        Name = name;
        Date = date;
    }

    public static Book New(BookId id, string name, DateTime date)
        => new Book(id, name, date);
    
    public void UpdateBookDetails(string name, DateTime date)
    {
        Name = name;
        Date = date;
    }
}