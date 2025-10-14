namespace Auction.Domain.Exceptions;

public class ErrorException : Exception
{
    public ErrorException(string errCode, string name, string message) : base($"{errCode}: {message}. \nCaller: {name}") { }
}

public class ErrorExceptions
{
    public static ErrorException InvalidLength<TCaller>(object property)
    {
        return new ErrorException(
            "ERR_INVALID_LENGTH", 
            typeof(TCaller).Name, 
            $"Invalid length for property '{property.GetType().Name}'.");
    }

    public static ErrorException InvalidFormat<TCaller>(object property)
    {
        return new ErrorException(
            "ERR_INVALID_FORMAT",
            typeof(TCaller).Name,
            $"Invalid format for property '{property.GetType().Name}'.");
    }

    public static ErrorException NullOrEmpty<TCaller>(object property)
    {
        return new ErrorException(
            "ERR_NULL_OR_EMPTY",
            typeof(TCaller).Name, 
            $"'{property.GetType().Name}' cannot be null or empty.");
    }
}
