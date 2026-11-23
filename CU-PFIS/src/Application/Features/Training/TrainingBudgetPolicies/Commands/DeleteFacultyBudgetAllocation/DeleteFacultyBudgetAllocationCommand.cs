using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.DeleteFacultyBudgetAllocation;

/// <summary>
/// Removes a custom per-faculty budget override so the faculty member reverts to the global fiscal-year policy.
/// </summary>
[RequirePermission(PermissionConstants.TrainingBudgetPolicies.Manage)]
public sealed record DeleteFacultyBudgetAllocationCommand(
    int FiscalYearBe,
    Guid FacultyMemberExternalId) : IRequest;

public sealed class DeleteFacultyBudgetAllocationCommandHandler
    : IRequestHandler<DeleteFacultyBudgetAllocationCommand>
{
    private readonly IChulaDbContext _context;

    public DeleteFacultyBudgetAllocationCommandHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteFacultyBudgetAllocationCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await _context.FacultyTrainingBudgetAllocations
            .Include(x => x.FacultyMember)
            .FirstOrDefaultAsync(
                x => x.FiscalYearBe == request.FiscalYearBe &&
                     x.FacultyMember.ExternalId == request.FacultyMemberExternalId,
                cancellationToken);

        if (existing is null)
        {
            return;
        }

        _context.FacultyTrainingBudgetAllocations.Remove(existing);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
