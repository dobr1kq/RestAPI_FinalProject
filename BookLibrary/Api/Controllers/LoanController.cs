using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Loans.Commands;
using Domain.Loans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("loans")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly ISender sender;
    private readonly ILoanQueries loanQueries;

    public LoansController(ISender sender, ILoanQueries loanQueries)
    {
        this.sender = sender;
        this.loanQueries = loanQueries;
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await loanQueries.GetAll(cancellationToken);
        return entities.Select(LoanDto.FromDomainModel).ToList();
    }
    
    [HttpGet("{loanId:guid}")]
    public async Task<ActionResult<LoanDto>> Get([FromRoute] Guid loanId, CancellationToken cancellationToken)
    {
        var entity = await loanQueries.GetById(new LoanId(loanId), cancellationToken);

        return entity.Match<ActionResult<LoanDto>>(
            l => LoanDto.FromDomainModel(l),
            () => NotFound());
    }
    
    [HttpPost]
    public async Task<ActionResult<LoanDto>> Create([FromBody] LoanDto request, CancellationToken cancellationToken)
    {
        var input = new CreateLoanCommand
        {
            ReaderId = request.ReaderId,
            BookId = request.BookId,
            LibrarianId = request.LibrarianId,
            LoanDate = request.LoanDate,
            ReturnDate = request.ReturnDate
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<LoanDto>>(
            l => LoanDto.FromDomainModel(l),
            e => e.ToObjectResult());
    }
    
    [HttpPut]
    public async Task<ActionResult<LoanDto>> Update([FromBody] LoanDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateLoanCommand
        {
            LoanId = request.Id,
            ReaderId = request.ReaderId,
            BookId = request.BookId,
            LibrarianId = request.LibrarianId,
            LoanDate = request.LoanDate,
            ReturnDate = request.ReturnDate
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<LoanDto>>(
            l => LoanDto.FromDomainModel(l),
            e => e.ToObjectResult());
    }
    
    [HttpDelete("{loanId:guid}")]
    public async Task<ActionResult<LoanDto>> Delete([FromRoute] Guid loanId, CancellationToken cancellationToken)
    {
        var input = new DeleteLoanCommand
        {
            LoanId = loanId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<LoanDto>>(
            l => LoanDto.FromDomainModel(l),
            e => e.ToObjectResult());
    }
}
