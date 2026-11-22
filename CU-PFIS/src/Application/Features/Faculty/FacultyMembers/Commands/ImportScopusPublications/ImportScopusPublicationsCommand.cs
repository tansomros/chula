using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Faculty.FacultyMembers.Commands.ImportScopusPublications;

public sealed record ImportScopusPublicationsCommand(
    Guid FacultyMemberExternalId,
    IReadOnlyList<ScopusPublicationImportItem> Publications,
    string SourceSystem = "ScopusCsv") : IRequest<ImportScopusPublicationsResult>;

public sealed record ScopusPublicationImportItem(
    string Title,
    string? JournalOrVenue,
    int PublishedYear,
    string? Doi,
    string? ScopusId);

public sealed record ImportScopusPublicationsResult(int AddedCount, int SkippedDuplicateCount);

public sealed class ImportScopusPublicationsCommandHandler
    : IRequestHandler<ImportScopusPublicationsCommand, ImportScopusPublicationsResult>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public ImportScopusPublicationsCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<ImportScopusPublicationsResult> Handle(
        ImportScopusPublicationsCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var member = await _context.FacultyMembers
            .Include(x => x.Department)
            .Include(x => x.Publications)
            .FirstOrDefaultAsync(x => x.ExternalId == request.FacultyMemberExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Faculty.FacultyMember), request.FacultyMemberExternalId);

        var isAuthorized = _currentUserService.IsInRole(RoleConstants.Admin) ||
            (_currentUserService.HasPermission(PermissionConstants.FacultyCv.EditAll) &&
             (!_currentUserService.DepartmentExternalId.HasValue ||
              _currentUserService.DepartmentExternalId.Value == member.Department.ExternalId)) ||
            (_currentUserService.HasPermission(PermissionConstants.FacultyCv.EditOwn) &&
             _currentUserService.FacultyMemberExternalId == member.ExternalId);

        if (!isAuthorized)
        {
            throw new ForbiddenAccessException("You do not have permission to import publications for this faculty member.");
        }

        var now = _timeProvider.GetUtcNow();
        var added = 0;
        var skipped = 0;

        foreach (var item in request.Publications)
        {
            if (member.TryImportPublication(
                item.Title,
                item.JournalOrVenue,
                item.PublishedYear,
                item.Doi,
                item.ScopusId,
                request.SourceSystem,
                now))
            {
                added++;
            }
            else
            {
                skipped++;
            }
        }

        if (added > 0)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        return new ImportScopusPublicationsResult(added, skipped);
    }
}
