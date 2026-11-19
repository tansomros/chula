using System.Diagnostics;
using Chula.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chula.Application.Common.Behaviours;

public sealed class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private const int SlowRequestThresholdMilliseconds = 500;
    private readonly Stopwatch _timer = new();
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public PerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > SlowRequestThresholdMilliseconds)
        {
            var requestName = typeof(TRequest).Name;
            var loginName = _currentUserService.LoginName ?? "Anonymous";

            // Never log full names or sensitive PII in plaintext application logs (per coding-rules.md)
            _logger.LogWarning(
                "CU-PFIS Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) by {LoginName}",
                requestName,
                elapsedMilliseconds,
                loginName);
        }

        return response;
    }
}
