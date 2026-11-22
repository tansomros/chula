using CUPPEP.Application.Common.Exceptions;
using CUPPEP.Application.Common.Interfaces;
using CUPPEP.Domain.Entities.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Features.Extensions.StudentProfiles;

public sealed record StudentProfileExtDto(
    Guid ExternalId,
    string StudentCode,
    string? AdmissionRoundCode,
    string? SeniorProjectTitleTh,
    string? SeniorProjectTitleEn,
    DateTimeOffset UpdatedAt);

public sealed record GetStudentProfilesQuery(DateTimeOffset? UpdatedSince = null)
    : IRequest<IReadOnlyList<StudentProfileExtDto>>;

public sealed class GetStudentProfilesQueryHandler
    : IRequestHandler<GetStudentProfilesQuery, IReadOnlyList<StudentProfileExtDto>>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentProfilesQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<IReadOnlyList<StudentProfileExtDto>> Handle(
        GetStudentProfilesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.StudentProfiles.AsNoTracking();
        if (request.UpdatedSince.HasValue)
        {
            var since = request.UpdatedSince.Value;
            query = query.Where(x => (x.LastModified ?? x.CreatedOn) >= since);
        }

        return await query
            .OrderBy(x => x.StudentCode)
            .Select(x => new StudentProfileExtDto(
                x.ExternalId,
                x.StudentCode,
                x.AdmissionRoundCode,
                x.SeniorProjectTitleTh,
                x.SeniorProjectTitleEn,
                x.LastModified ?? x.CreatedOn))
            .ToListAsync(cancellationToken);
    }
}

public sealed record GetStudentProfileByCodeQuery(string StudentCode) : IRequest<StudentProfileExtDto>;

public sealed class GetStudentProfileByCodeQueryHandler
    : IRequestHandler<GetStudentProfileByCodeQuery, StudentProfileExtDto>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentProfileByCodeQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<StudentProfileExtDto> Handle(
        GetStudentProfileByCodeQuery request,
        CancellationToken cancellationToken)
    {
        var code = request.StudentCode.Trim();
        return await _context.StudentProfiles
            .AsNoTracking()
            .Where(x => x.StudentCode == code)
            .Select(x => new StudentProfileExtDto(
                x.ExternalId,
                x.StudentCode,
                x.AdmissionRoundCode,
                x.SeniorProjectTitleTh,
                x.SeniorProjectTitleEn,
                x.LastModified ?? x.CreatedOn))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(StudentProfileExt), code);
    }
}

public sealed record UpsertStudentProfileCommand(
    string StudentCode,
    string? AdmissionRoundCode,
    string? SeniorProjectTitleTh,
    string? SeniorProjectTitleEn) : IRequest<StudentProfileExtDto>;

public sealed class UpsertStudentProfileCommandHandler
    : IRequestHandler<UpsertStudentProfileCommand, StudentProfileExtDto>
{
    private readonly ICuPpepDbContext _context;

    public UpsertStudentProfileCommandHandler(ICuPpepDbContext context) => _context = context;

    public async Task<StudentProfileExtDto> Handle(
        UpsertStudentProfileCommand request,
        CancellationToken cancellationToken)
    {
        var code = request.StudentCode.Trim();
        var entity = await _context.StudentProfiles
            .FirstOrDefaultAsync(x => x.StudentCode == code, cancellationToken);

        if (entity is null)
        {
            entity = new StudentProfileExt(
                code,
                request.AdmissionRoundCode,
                request.SeniorProjectTitleTh,
                request.SeniorProjectTitleEn);
            _context.StudentProfiles.Add(entity);
        }
        else
        {
            entity.Update(
                request.AdmissionRoundCode,
                request.SeniorProjectTitleTh,
                request.SeniorProjectTitleEn);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new StudentProfileExtDto(
            entity.ExternalId,
            entity.StudentCode,
            entity.AdmissionRoundCode,
            entity.SeniorProjectTitleTh,
            entity.SeniorProjectTitleEn,
            entity.EffectiveUpdatedAt);
    }
}
