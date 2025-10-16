namespace RealtimeAuction.Exceptions.Exceptions;

public class DatabaseExceptions
{
    public static ErrorException EntryValueNotUnique<TCaller>(string property)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_DB_VALUE_NOT_UNIQUE", 
            $"Database already contains '{property}' with the same value and the configuration is set to unique.");
    }
    
    public static ErrorException EntryNonExistent<TCaller>(string property, string value)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_DB_ENTRY_NON_EXISTENT", 
            $"Database does not contain entry where '{property}' equals '{value}'.");
    }
}