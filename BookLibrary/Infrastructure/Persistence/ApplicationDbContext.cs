using System.Reflection;
using Domain.Authors;
using Domain.Books;
using Domain.Genres;
using Domain.Librarians;
using Domain.Loans;
using Domain.Publishers;
using Domain.Readers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Librarian> Librarians { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Reader> Readers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}