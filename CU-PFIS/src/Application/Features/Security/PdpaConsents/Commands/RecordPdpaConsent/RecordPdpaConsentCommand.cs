using Chula.Application.Common.Interfaces;
using Chula.Domain.Entities.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Security.PdpaConsents.Commands.RecordPdpaConsent;

public sealed record RecordPdpaConsentCommand(
    string PolicyVersion,
    string? IpAddress = null,
    string? UserAgent = null) : IRequest<PdpaConsentStatusDto>;

public sealed record PdpaConsentStatusDto(
    Guid ExternalId,
    string PolicyVersion,
    bool HasAccepted,
    DateTimeOffset? AcceptedAt);

public sealed class RecordPdpaConsentCommandHandler : IRequestHandler<RecordPdpaConsentCommand, PdpaConsentStatusDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public RecordPdpaConsentCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<PdpaConsentStatusDto> Handle(
        RecordPdpaConsentCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var userId = _currentUserService.UserId;
        var loginName = _currentUserService.LoginName ?? userId;
        var normalizedVersion = request.PolicyVersion.Trim();

        var existing = await _context.PdpaConsents
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.UserId == userId && x.PolicyVersion == normalizedVersion,
                cancellationToken);

        if (existing is not null)
        {
            return new PdpaConsentStatusDto(
                existing.ExternalId,
                existing.PolicyVersion,
                true,
                existing.AcceptedAt);
        }

        var consent = new PdpaConsent(
            userId,
            loginName,
            normalizedVersion,
            _timeProvider.GetUtcNow(),
            request.IpAddress,
            request.UserAgent);

        _context.PdpaConsents.Add(consent);
        await _context.SaveChangesAsync(cancellationToken);

        return new PdpaConsentStatusDto(
            consent.ExternalId,
            consent.PolicyVersion,
            true,
            consent.AcceptedAt);
    }
}
