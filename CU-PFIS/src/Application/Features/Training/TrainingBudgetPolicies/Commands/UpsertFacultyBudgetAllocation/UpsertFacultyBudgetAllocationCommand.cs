using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.Faculty;
using Chula.Domain.Entities.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertFacultyBudgetAllocation;

[RequirePermission(PermissionConstants.TrainingBudgetPolicies.Manage)]
public sealed record UpsertFacultyBudgetAllocationCommand(
    int FiscalYearBe,
    Guid FacultyMemberExternalId,
    decimal AnnualQuotaAmount,
    decimal? MaxBorrowAmount = null,
    string? Note = null) : IRequest<Guid>;

public sealed class UpsertFacultyBudgetAllocationCommandHandler
    : IRequestHandler<UpsertFacultyBudgetAllocationCommand, Guid>
{
    private readonly IChulaDbContext _context;

    public UpsertFacultyBudgetAllocationCommandHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        UpsertFacultyBudgetAllocationCommand request,
        CancellationToken cancellationToken)
    {
        var faculty = await _context.FacultyMembers
            .FirstOrDefaultAsync(x => x.ExternalId == request.FacultyMemberExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(FacultyMember), request.FacultyMemberExternalId);

        var existing = await _context.FacultyTrainingBudgetAllocations
            .FirstOrDefaultAsync(
                x => x.FacultyMemberId == faculty.Id && x.FiscalYearBe == request.FiscalYearBe,
                cancellationToken);

        if (existing is null)
        {
            existing = new FacultyTrainingBudgetAllocation(
                faculty.Id,
                request.FiscalYearBe,
                request.AnnualQuotaAmount,
                request.MaxBorrowAmount,
                request.Note);
            _context.FacultyTrainingBudgetAllocations.Add(existing);
        }
        else
        {
            existing.UpdateAllocation(
                request.AnnualQuotaAmount,
                request.MaxBorrowAmount,
                request.Note);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return existing.ExternalId;
    }
}
