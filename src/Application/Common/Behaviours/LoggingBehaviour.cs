using Chula.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chula.Application.Common.Behaviours;

public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var loginName = _currentUserService.LoginName ?? "Anonymous";

        // Log only request type and LoginName (never PII per coding-rules.md)
        _logger.LogInformation("CU-PFIS Request: {Name} by {LoginName}", requestName, loginName);

        return await next();
    }
}
