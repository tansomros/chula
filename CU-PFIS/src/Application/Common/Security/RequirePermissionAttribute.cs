using Ardalis.GuardClauses;

namespace Chula.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public sealed class RequirePermissionAttribute : Attribute
{
    public RequirePermissionAttribute(string permission)
    {
        Permission = Guard.Against.NullOrWhiteSpace(permission, nameof(permission)).Trim().ToLowerInvariant();
    }

    public string Permission { get; }
}
