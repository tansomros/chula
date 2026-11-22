using CUPPEP.Application.Common.Exceptions;
using CUPPEP.Application.Common.Interfaces;
using CUPPEP.Domain.Entities.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Features.Extensions.StudentEmployments;

public sealed record StudentEmploymentRound1ExtDto(
    Guid ExternalId,
    string StudentCode,
    int GraduationYearBe,
    string EmploymentStatus,
    string? JobTitle,
    string? WorkplaceName,
    string? SurveyAnswersJson,
    DateTimeOffset UpdatedAt);

public sealed record GetStudentEmploymentsQuery(DateTimeOffset? UpdatedSince = null)
    : IRequest<IReadOnlyList<StudentEmploymentRound1ExtDto>>;

public sealed class GetStudentEmploymentsQueryHandler
    : IRequestHandler<GetStudentEmploymentsQuery, IReadOnlyList<StudentEmploymentRound1ExtDto>>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentEmploymentsQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<IReadOnlyList<StudentEmploymentRound1ExtDto>> Handle(
        GetStudentEmploymentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.StudentEmploymentsRound1.AsNoTracking();
        if (request.UpdatedSince.HasValue)
        {
            var since = request.UpdatedSince.Value;
            query = query.Where(x => (x.LastModified ?? x.CreatedOn) >= since);
        }

        return await query
            .OrderBy(x => x.StudentCode)
            .Select(x => new StudentEmploymentRound1ExtDto(
                x.ExternalId,
                x.StudentCode,
                x.GraduationYearBe,
                x.EmploymentStatus,
                x.JobTitle,
                x.WorkplaceName,
                x.SurveyAnswersJson,
                x.LastModified ?? x.CreatedOn))
            .ToListAsync(cancellationToken);
    }
}

public sealed record GetStudentEmploymentByCodeQuery(string StudentCode) : IRequest<StudentEmploymentRound1ExtDto>;

public sealed class GetStudentEmploymentByCodeQueryHandler
    : IRequestHandler<GetStudentEmploymentByCodeQuery, StudentEmploymentRound1ExtDto>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentEmploymentByCodeQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<StudentEmploymentRound1ExtDto> Handle(
        GetStudentEmploymentByCodeQuery request,
        CancellationToken cancellationToken)
    {
        var code = request.StudentCode.Trim();
        return await _context.StudentEmploymentsRound1
            .AsNoTracking()
            .Where(x => x.StudentCode == code)
            .Select(x => new StudentEmploymentRound1ExtDto(
                x.ExternalId,
                x.StudentCode,
                x.GraduationYearBe,
                x.EmploymentStatus,
                x.JobTitle,
                x.WorkplaceName,
                x.SurveyAnswersJson,
                x.LastModified ?? x.CreatedOn))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(StudentEmploymentRound1Ext), code);
    }
}

public sealed record UpsertStudentEmploymentCommand(
    string StudentCode,
    int GraduationYearBe,
    string EmploymentStatus,
    string? JobTitle = null,
    string? WorkplaceName = null,
    string? SurveyAnswersJson = null) : IRequest<StudentEmploymentRound1ExtDto>;

public sealed class UpsertStudentEmploymentCommandHandler
    : IRequestHandler<UpsertStudentEmploymentCommand, StudentEmploymentRound1ExtDto>
{
    private readonly ICuPpepDbContext _context;

    public UpsertStudentEmploymentCommandHandler(ICuPpepDbContext context) => _context = context;

    public async Task<StudentEmploymentRound1ExtDto> Handle(
        UpsertStudentEmploymentCommand request,
        CancellationToken cancellationToken)
    {
        var code = request.StudentCode.Trim();
        var entity = await _context.StudentEmploymentsRound1
            .FirstOrDefaultAsync(x => x.StudentCode == code, cancellationToken);

        if (entity is null)
        {
            entity = new StudentEmploymentRound1Ext(
                code,
                request.GraduationYearBe,
                request.EmploymentStatus,
                request.JobTitle,
                request.WorkplaceName,
                request.SurveyAnswersJson);
            _context.StudentEmploymentsRound1.Add(entity);
        }
        else
        {
            entity.Update(
                request.GraduationYearBe,
                request.EmploymentStatus,
                request.JobTitle,
                request.WorkplaceName,
                request.SurveyAnswersJson);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new StudentEmploymentRound1ExtDto(
            entity.ExternalId,
            entity.StudentCode,
            entity.GraduationYearBe,
            entity.EmploymentStatus,
            entity.JobTitle,
            entity.WorkplaceName,
            entity.SurveyAnswersJson,
            entity.EffectiveUpdatedAt);
    }
}
