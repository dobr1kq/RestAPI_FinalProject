using Domain.Readers;

namespace Api.Dtos;

public record ReaderDto(
    Guid Id,
    string FirstName,
    string LastName,
    string TelephoneNumber)
{
    public static ReaderDto FromDomainModel(Reader reader)
        => new(
            Id: reader.Id.Value,
            FirstName: reader.FirstName,
            LastName: reader.LastName,
            TelephoneNumber: reader.TelephoneNumber);
}