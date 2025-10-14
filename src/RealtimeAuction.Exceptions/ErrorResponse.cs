namespace RealtimeAuction.Exceptions;

public record ErrorResponse(string ErrorCode, string Caller, string Message);
