namespace RealtimeAuction.Exceptions;

public class ErrorException : Exception 
{
    public string ErrCode { get; }
    public string Message { get; }
    public string Caller { get; }

    public ErrorException(string errCode, string message, string caller) : base($"{errCode} || {message} || {caller}")
    {
        ErrCode = errCode;
        Message = message;
        Caller = caller;
    }
}

public class ErrorExceptionWithCaller<TCaller> : ErrorException
{
    public ErrorExceptionWithCaller(string errCode, string message) : base(errCode, message, typeof(TCaller).Name) { }
}