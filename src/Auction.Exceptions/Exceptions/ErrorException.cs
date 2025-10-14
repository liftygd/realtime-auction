namespace Auction.Exceptions.Exceptions;

public class ErrorException : Exception 
{
    public ErrorException(string message) : base(message) { }
}

public class ErrorExceptionWithCaller<TCaller> : ErrorException
{
    public ErrorExceptionWithCaller(string errCode, string message) : base($"{errCode} || {message} || {typeof(TCaller).Name}") { }
}

public class ErrorExceptions
{
    public static ErrorException InvalidLength<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_INVALID_LENGTH", 
            $"Invalid length for property '{property}'.");
    }

    public static ErrorException InvalidFormat<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_INVALID_FORMAT",
            $"Invalid format for property '{property}'.");
    }

    public static ErrorException ZeroOrNegative<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_NON_NEGATIVE",
            $"'{property}' must be higher than zero.");
    }

    public static ErrorException NullOrEmpty<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_NULL_OR_EMPTY",
            $"'{property}' cannot be null or empty.");
    }
}
