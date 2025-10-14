namespace RealtimeAuction.Exceptions.Exceptions;

public class ErrorExceptions
{
    public static ErrorException StringTooLong<TCaller>(string property, int maxLength)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_STRING_TOO_LONG", 
            $"Invalid length for property '{property}'. Max allowed length is {maxLength}.");
    }

    public static ErrorException ValueOutsideBounds<TCaller>(string property, int minBound, int maxBound)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_VALUE_OUTSIDE_BOUNDS", 
            $"Value of '{property}' is outside bounds ({minBound} - {maxBound}).");
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
            "ERR_ZERO_OR_NEGATIVE",
            $"'{property}' must be higher than zero.");
    }
    
    public static ErrorException Negative<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_NEGATIVE",
            $"'{property}' must be positive.");
    }

    public static ErrorException NullOrEmpty<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_NULL_OR_EMPTY",
            $"'{property}' cannot be null or empty.");
    }
}