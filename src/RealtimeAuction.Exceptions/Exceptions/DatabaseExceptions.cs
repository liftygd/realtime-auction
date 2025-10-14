namespace RealtimeAuction.Exceptions.Exceptions;

public class DatabaseExceptions
{
    public static ErrorException DatabaseValueNotUnique<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_DB_VALUE_NOT_UNIQUE", 
            $"Database already contains '{property}' with the same value and the configuration is set to unique.");
    }
}