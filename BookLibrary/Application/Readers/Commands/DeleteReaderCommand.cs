using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Common.Readers.Exceptions;
using Domain.Readers;
using MediatR;

namespace Application.Readers.Commands;

public record DeleteReaderCommand : IRequest<Result<Reader, ReaderException>>
{
    public required Guid ReaderId { get; init; }
}
public class DeleteReaderCommandHandler : IRequestHandler<DeleteReaderCommand, Result<Reader, ReaderException>>
{
    private readonly IReaderRepository readerRepository;

    public DeleteReaderCommandHandler(IReaderRepository readerRepository)
    {
        this.readerRepository = readerRepository;
    }

    public async Task<Result<Reader, ReaderException>> Handle(DeleteReaderCommand request, CancellationToken cancellationToken)
    {
        var readerId = new ReaderId(request.ReaderId);

        var existingReader = await readerRepository.GetById(readerId, cancellationToken);

        return await existingReader.Match<Task<Result<Reader, ReaderException>>>(
            async r => await DeleteEntity(r, cancellationToken),
            () => Task.FromResult<Result<Reader, ReaderException>>(new ReaderNotFoundException(readerId))
        );
    }

    private async Task<Result<Reader, ReaderException>> DeleteEntity(Reader reader, CancellationToken cancellationToken)
    {
        try
        {
            return await readerRepository.Delete(reader, cancellationToken);
        }
        catch (Exception ex)
        {
            return new ReaderUnknownException(reader.Id, ex);
        }
    }
}
