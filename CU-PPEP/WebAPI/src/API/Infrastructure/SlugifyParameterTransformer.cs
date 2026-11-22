using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace CUPPEP.API.Infrastructure;

public sealed partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value is null) return null;
        return KebabCaseRegex().Replace(value.ToString()!, "$1-$2").ToLowerInvariant();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex KebabCaseRegex();
}
