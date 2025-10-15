namespace RealtimeAuction.Exceptions;

public class ErrorException : Exception 
{
    public string ErrCode { get; }
    public string ErrMessage { get; }
    public string Caller { get; }

    public ErrorException(string errCode, string errMessage, string caller) : base($"{errCode} || {errMessage} || {caller}")
    {
        ErrCode = errCode;
        ErrMessage = errMessage;
        Caller = caller;
    }
}

public class ErrorExceptionWithCaller<TCaller> : ErrorException
{
    public ErrorExceptionWithCaller(string errCode, string errMessage) : base(errCode, errMessage, typeof(TCaller).Name) { }
}