using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Common.Readers.Exceptions;
using Domain.Readers;
using MediatR;

namespace Application.Readers.Commands;

public record CreateReaderCommand : IRequest<Result<Reader, ReaderException>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string TelephoneNumber { get; init; }
}
public class CreateReaderCommandHandler : IRequestHandler<CreateReaderCommand, Result<Reader, ReaderException>>
{
    private readonly IReaderRepository readerRepository;

    public CreateReaderCommandHandler(IReaderRepository readerRepository)
    {
        this.readerRepository = readerRepository;
    }

    public async Task<Result<Reader, ReaderException>> Handle(CreateReaderCommand request, CancellationToken cancellationToken)
    {
        var existingReader = await readerRepository.GetByName(request.FirstName, request.LastName, cancellationToken);

        return await existingReader.Match<Task<Result<Reader, ReaderException>>>(
            r => Task.FromResult<Result<Reader, ReaderException>>(new ReaderAlreadyExistsException(r.Id)),
            async () => await CreateEntity(request.FirstName, request.LastName, request.TelephoneNumber, cancellationToken)
        );
    }

    private async Task<Result<Reader, ReaderException>> CreateEntity(string firstName, string lastName, string telephoneNumber, 
        CancellationToken cancellationToken)
    {
        try
        {
            var readerId = ReaderId.New();
            var reader = Reader.Create(readerId, firstName, lastName, telephoneNumber);
            return await readerRepository.Add(reader, cancellationToken);
        }
        catch (Exception ex)
        {
            return new ReaderUnknownException(ReaderId.Empty(), ex);
        }
    }
}
