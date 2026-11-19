using System.Reflection;
using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;

namespace Chula.Application.Common.Behaviours;

public sealed class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var permissionAttributes = request.GetType().GetCustomAttributes<RequirePermissionAttribute>().ToList();

        if (permissionAttributes.Count > 0)
        {
            if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
            {
                throw new UnauthorizedAccessException();
            }

            if (!_currentUserService.IsInRole(RoleConstants.Admin))
            {
                foreach (var attribute in permissionAttributes)
                {
                    if (!_currentUserService.HasPermission(attribute.Permission))
                    {
                        throw new ForbiddenAccessException($"Missing required permission: '{attribute.Permission}'.");
                    }
                }
            }
        }

        return await next();
    }
}
