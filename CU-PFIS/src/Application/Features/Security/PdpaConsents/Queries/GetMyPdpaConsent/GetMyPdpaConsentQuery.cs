using Chula.Application.Common.Interfaces;
using Chula.Application.Features.Security.PdpaConsents.Commands.RecordPdpaConsent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Security.PdpaConsents.Queries.GetMyPdpaConsent;

public sealed record GetMyPdpaConsentQuery(string PolicyVersion = "2026.1") : IRequest<PdpaConsentStatusDto>;

public sealed class GetMyPdpaConsentQueryHandler : IRequestHandler<GetMyPdpaConsentQuery, PdpaConsentStatusDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetMyPdpaConsentQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PdpaConsentStatusDto> Handle(
        GetMyPdpaConsentQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var normalizedVersion = request.PolicyVersion.Trim();
        var consent = await _context.PdpaConsents
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.UserId == _currentUserService.UserId && x.PolicyVersion == normalizedVersion,
                cancellationToken);

        return consent is null
            ? new PdpaConsentStatusDto(Guid.Empty, normalizedVersion, false, null)
            : new PdpaConsentStatusDto(consent.ExternalId, consent.PolicyVersion, true, consent.AcceptedAt);
    }
}
