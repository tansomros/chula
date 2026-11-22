using CUPPEP.Application.Common.Exceptions;
using CUPPEP.Application.Common.Interfaces;
using CUPPEP.Domain.Entities.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Features.Extensions.StudentLicenseResults;

public sealed record StudentLicenseResultExtDto(
    Guid ExternalId,
    string StudentCode,
    bool PassedCc1,
    bool PassedCc2,
    bool PassedIp1,
    bool PassedIp2,
    bool PassedPc1,
    bool PassedPc2,
    DateTimeOffset UpdatedAt);

public sealed record GetStudentLicenseResultsQuery(DateTimeOffset? UpdatedSince = null)
    : IRequest<IReadOnlyList<StudentLicenseResultExtDto>>;

public sealed class GetStudentLicenseResultsQueryHandler
    : IRequestHandler<GetStudentLicenseResultsQuery, IReadOnlyList<StudentLicenseResultExtDto>>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentLicenseResultsQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<IReadOnlyList<StudentLicenseResultExtDto>> Handle(
        GetStudentLicenseResultsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.StudentLicenseResults.AsNoTracking();
        if (request.UpdatedSince.HasValue)
        {
            var since = request.UpdatedSince.Value;
            query = query.Where(x => (x.LastModified ?? x.CreatedOn) >= since);
        }

        return await query
            .OrderBy(x => x.StudentCode)
            .Select(x => new StudentLicenseResultExtDto(
                x.ExternalId,
                x.StudentCode,
                x.PassedCc1,
                x.PassedCc2,
                x.PassedIp1,
                x.PassedIp2,
                x.PassedPc1,
                x.PassedPc2,
                x.LastModified ?? x.CreatedOn))
            .ToListAsync(cancellationToken);
    }
}

public sealed record GetStudentLicenseResultByCodeQuery(string StudentCode) : IRequest<StudentLicenseResultExtDto>;

public sealed class GetStudentLicenseResultByCodeQueryHandler
    : IRequestHandler<GetStudentLicenseResultByCodeQuery, StudentLicenseResultExtDto>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentLicenseResultByCodeQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<StudentLicenseResultExtDto> Handle(
        GetStudentLicenseResultByCodeQuery request,
        CancellationToken cancellationToken)
    {
        var code = request.StudentCode.Trim();
        return await _context.StudentLicenseResults
            .AsNoTracking()
            .Where(x => x.StudentCode == code)
            .Select(x => new StudentLicenseResultExtDto(
                x.ExternalId,
                x.StudentCode,
                x.PassedCc1,
                x.PassedCc2,
                x.PassedIp1,
                x.PassedIp2,
                x.PassedPc1,
                x.PassedPc2,
                x.LastModified ?? x.CreatedOn))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(StudentLicenseResultExt), code);
    }
}

public sealed record UpsertStudentLicenseResultCommand(
    string StudentCode,
    bool PassedCc1,
    bool PassedCc2,
    bool PassedIp1,
    bool PassedIp2,
    bool PassedPc1,
    bool PassedPc2) : IRequest<StudentLicenseResultExtDto>;

public sealed class UpsertStudentLicenseResultCommandHandler
    : IRequestHandler<UpsertStudentLicenseResultCommand, StudentLicenseResultExtDto>
{
    private readonly ICuPpepDbContext _context;

    public UpsertStudentLicenseResultCommandHandler(ICuPpepDbContext context) => _context = context;

    public async Task<StudentLicenseResultExtDto> Handle(
        UpsertStudentLicenseResultCommand request,
        CancellationToken cancellationToken)
    {
        var code = request.StudentCode.Trim();
        var entity = await _context.StudentLicenseResults
            .FirstOrDefaultAsync(x => x.StudentCode == code, cancellationToken);

        if (entity is null)
        {
            entity = new StudentLicenseResultExt(
                code,
                request.PassedCc1,
                request.PassedCc2,
                request.PassedIp1,
                request.PassedIp2,
                request.PassedPc1,
                request.PassedPc2);
            _context.StudentLicenseResults.Add(entity);
        }
        else
        {
            entity.Update(
                request.PassedCc1,
                request.PassedCc2,
                request.PassedIp1,
                request.PassedIp2,
                request.PassedPc1,
                request.PassedPc2);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new StudentLicenseResultExtDto(
            entity.ExternalId,
            entity.StudentCode,
            entity.PassedCc1,
            entity.PassedCc2,
            entity.PassedIp1,
            entity.PassedIp2,
            entity.PassedPc1,
            entity.PassedPc2,
            entity.EffectiveUpdatedAt);
    }
}
