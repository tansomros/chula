using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Chula.API.Infrastructure;

public sealed partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null)
        {
            return null;
        }

        var text = value.ToString();
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        return KebabCaseRegex().Replace(text, "$1-$2").ToLowerInvariant();
    }

    [GeneratedRegex("([a-z0-9])([A-Z])")]
    private static partial Regex KebabCaseRegex();
}
