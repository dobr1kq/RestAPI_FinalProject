using Domain.Librarians;

namespace Api.Dtos;

public record LibrarianDto(
    Guid Id,
    string FirstName,
    string LastName,
    string TelephoneNumber)
{
    public static LibrarianDto FromDomainModel(Librarian librarian)
        => new(
            Id: librarian.Id.Value,
            FirstName: librarian.FirstName,
            LastName: librarian.LastName,
            TelephoneNumber: librarian.TelephoneNumber);
}