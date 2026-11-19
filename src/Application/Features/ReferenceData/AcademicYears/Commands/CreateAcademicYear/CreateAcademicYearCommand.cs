using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.ReferenceData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.ReferenceData.AcademicYears.Commands.CreateAcademicYear;

[RequirePermission(PermissionConstants.ReferenceData.Manage)]
public sealed record CreateAcademicYearCommand(
    int YearBe,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsCurrent = false) : IRequest<Guid>;

public sealed class CreateAcademicYearCommandHandler : IRequestHandler<CreateAcademicYearCommand, Guid>
{
    private readonly IChulaDbContext _context;

    public CreateAcademicYearCommandHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAcademicYearCommand request, CancellationToken cancellationToken)
    {
        if (request.IsCurrent)
        {
            var currentYears = await _context.AcademicYears
                .Where(x => x.IsCurrent)
                .ToListAsync(cancellationToken);

            foreach (var year in currentYears)
            {
                year.UnmarkAsCurrent();
            }
        }

        var entity = new AcademicYear(
            request.YearBe,
            request.StartDate,
            request.EndDate,
            request.IsCurrent);

        _context.AcademicYears.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.ExternalId;
    }
}
