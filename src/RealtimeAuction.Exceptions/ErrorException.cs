namespace RealtimeAuction.Exceptions.Exceptions;

public class ErrorException : Exception 
{
    public ErrorException(string message) : base(message) { }
}

public class ErrorExceptionWithCaller<TCaller> : ErrorException
{
    public ErrorExceptionWithCaller(string errCode, string message) : base($"{errCode} || {message} || {typeof(TCaller).Name}") { }
}