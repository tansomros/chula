using CUPPEP.Application.Common.Exceptions;
using CUPPEP.Application.Common.Interfaces;
using CUPPEP.Domain.Entities.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Features.Extensions.StudentAchievements;

public sealed record StudentAchievementExtDto(
    Guid ExternalId,
    string StudentCode,
    int AcademicYearBe,
    int StudyYear,
    string EventName,
    string AwardName,
    DateOnly EventDate,
    decimal? GrantAmount,
    bool IsDeleted,
    DateTimeOffset UpdatedAt);

public sealed record GetStudentAchievementsQuery(
    string? StudentCode = null,
    DateTimeOffset? UpdatedSince = null,
    bool IncludeDeleted = false) : IRequest<IReadOnlyList<StudentAchievementExtDto>>;

public sealed class GetStudentAchievementsQueryHandler
    : IRequestHandler<GetStudentAchievementsQuery, IReadOnlyList<StudentAchievementExtDto>>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentAchievementsQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<IReadOnlyList<StudentAchievementExtDto>> Handle(
        GetStudentAchievementsQuery request,
        CancellationToken cancellationToken)
    {
        var query = request.IncludeDeleted
            ? _context.StudentAchievements.IgnoreQueryFilters().AsNoTracking()
            : _context.StudentAchievements.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.StudentCode))
        {
            var code = request.StudentCode.Trim();
            query = query.Where(x => x.StudentCode == code);
        }

        if (request.UpdatedSince.HasValue)
        {
            var since = request.UpdatedSince.Value;
            query = query.Where(x => (x.LastModified ?? x.CreatedOn) >= since);
        }

        return await query
            .OrderByDescending(x => x.EventDate)
            .ThenBy(x => x.Id)
            .Select(x => new StudentAchievementExtDto(
                x.ExternalId,
                x.StudentCode,
                x.AcademicYearBe,
                x.StudyYear,
                x.EventName,
                x.AwardName,
                x.EventDate,
                x.GrantAmount,
                x.IsDeleted,
                x.LastModified ?? x.CreatedOn))
            .ToListAsync(cancellationToken);
    }
}

public sealed record GetStudentAchievementByIdQuery(Guid ExternalId) : IRequest<StudentAchievementExtDto>;

public sealed class GetStudentAchievementByIdQueryHandler
    : IRequestHandler<GetStudentAchievementByIdQuery, StudentAchievementExtDto>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentAchievementByIdQueryHandler(ICuPpepDbContext context) => _context = context;

    public async Task<StudentAchievementExtDto> Handle(
        GetStudentAchievementByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.StudentAchievements
            .AsNoTracking()
            .Where(x => x.ExternalId == request.ExternalId)
            .Select(x => new StudentAchievementExtDto(
                x.ExternalId,
                x.StudentCode,
                x.AcademicYearBe,
                x.StudyYear,
                x.EventName,
                x.AwardName,
                x.EventDate,
                x.GrantAmount,
                x.IsDeleted,
                x.LastModified ?? x.CreatedOn))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(StudentAchievementExt), request.ExternalId);
    }
}

public sealed record CreateStudentAchievementCommand(
    string StudentCode,
    int AcademicYearBe,
    int StudyYear,
    string EventName,
    string AwardName,
    DateOnly EventDate,
    decimal? GrantAmount = null) : IRequest<Guid>;

public sealed class CreateStudentAchievementCommandHandler
    : IRequestHandler<CreateStudentAchievementCommand, Guid>
{
    private readonly ICuPpepDbContext _context;

    public CreateStudentAchievementCommandHandler(ICuPpepDbContext context) => _context = context;

    public async Task<Guid> Handle(
        CreateStudentAchievementCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new StudentAchievementExt(
            request.StudentCode,
            request.AcademicYearBe,
            request.StudyYear,
            request.EventName,
            request.AwardName,
            request.EventDate,
            request.GrantAmount);

        _context.StudentAchievements.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.ExternalId;
    }
}

public sealed record UpdateStudentAchievementCommand(
    Guid ExternalId,
    int AcademicYearBe,
    int StudyYear,
    string EventName,
    string AwardName,
    DateOnly EventDate,
    decimal? GrantAmount = null) : IRequest;

public sealed class UpdateStudentAchievementCommandHandler
    : IRequestHandler<UpdateStudentAchievementCommand>
{
    private readonly ICuPpepDbContext _context;

    public UpdateStudentAchievementCommandHandler(ICuPpepDbContext context) => _context = context;

    public async Task Handle(UpdateStudentAchievementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StudentAchievements
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(StudentAchievementExt), request.ExternalId);

        entity.Update(
            request.AcademicYearBe,
            request.StudyYear,
            request.EventName,
            request.AwardName,
            request.EventDate,
            request.GrantAmount);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public sealed record DeleteStudentAchievementCommand(Guid ExternalId) : IRequest;

public sealed class DeleteStudentAchievementCommandHandler
    : IRequestHandler<DeleteStudentAchievementCommand>
{
    private readonly ICuPpepDbContext _context;

    public DeleteStudentAchievementCommandHandler(ICuPpepDbContext context) => _context = context;

    public async Task Handle(DeleteStudentAchievementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StudentAchievements
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(StudentAchievementExt), request.ExternalId);

        _context.StudentAchievements.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
