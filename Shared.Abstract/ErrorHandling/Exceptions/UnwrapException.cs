namespace TaskNotFound.Shared.Abstract.ErrorHandling.Exceptions;

public sealed class UnwrapException : Exception
{
    internal UnwrapException(string? message) : base(message)
    {
    }
}
