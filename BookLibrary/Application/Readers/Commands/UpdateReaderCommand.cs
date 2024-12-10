using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Common.Readers.Exceptions;
using Domain.Readers;
using MediatR;

namespace Application.Readers.Commands;

public record UpdateReaderCommand : IRequest<Result<Reader, ReaderException>>
{
    public required Guid ReaderId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string TelephoneNumber { get; init; }
}
public class UpdateReaderCommandHandler : IRequestHandler<UpdateReaderCommand, Result<Reader, ReaderException>>
{
    private readonly IReaderRepository readerRepository;

    public UpdateReaderCommandHandler(IReaderRepository readerRepository)
    {
        this.readerRepository = readerRepository;
    }

    public async Task<Result<Reader, ReaderException>> Handle(UpdateReaderCommand request, CancellationToken cancellationToken)
    {
        var readerId = new ReaderId(request.ReaderId);

        var existingReader = await readerRepository.GetById(readerId, cancellationToken);

        return await existingReader.Match<Task<Result<Reader, ReaderException>>>(
            async r => await UpdateEntity(r, request.FirstName, request.LastName, request.TelephoneNumber, cancellationToken),
            () => Task.FromResult<Result<Reader, ReaderException>>(new ReaderNotFoundException(readerId))
        );
    }

    private async Task<Result<Reader, ReaderException>> UpdateEntity(
        Reader reader,
        string firstName,
        string lastName,
        string telephoneNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            reader.UpdateDetails(firstName, lastName, telephoneNumber);

            return await readerRepository.Update(reader, cancellationToken);
        }
        catch (Exception ex)
        {
            return new ReaderUnknownException(reader.Id, ex);
        }
    }
}
