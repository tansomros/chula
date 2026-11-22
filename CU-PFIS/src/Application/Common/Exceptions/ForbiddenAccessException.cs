namespace Chula.Application.Common.Exceptions;

public sealed class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
        : base("Forbidden access.")
    {
    }

    public ForbiddenAccessException(string message)
        : base(message)
    {
    }
}
