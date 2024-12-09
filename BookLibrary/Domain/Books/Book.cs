namespace Domain.Books;

public class Book
{
    public BookId Id { get; }
    public string Name {get; private set;} 
    public DateTime Date {get; private set;}
    
}