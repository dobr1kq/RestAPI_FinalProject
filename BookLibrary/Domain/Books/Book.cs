using Domain.Authors;
using Domain.Genres;
using Domain.Publishers;

namespace Domain.Books;

public class Book
{
    public BookId Id { get; }
    
    public AuthorId AuthorId { get; private set; }
    
    public GenreId GenreId { get; private set; }
    
    public PublisherId PublisherId { get; private set; }
    public string Name {get; private set;} 
    public DateTime Date {get; private set;}
    
    private Book(BookId id, string name, DateTime date, GenreId genreId, AuthorId authorId, PublisherId publisherId)
    {
        Id = id;
        Name = name;
        Date = date;
        GenreId = genreId;
        AuthorId = authorId;
        PublisherId = publisherId;
    }

    public static Book New(BookId id, string name, DateTime date, GenreId genreId, AuthorId authorId, PublisherId publisherId)
        => new Book(id, name, date, genreId, authorId, publisherId);
    
    public void UpdateBookDetails(string name, DateTime date, GenreId genreId, AuthorId authorId, PublisherId publisherId)
    {
        Name = name;
        Date = date;
        GenreId = genreId;
        AuthorId = authorId;
        PublisherId = publisherId;
    }
}