using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace RealtimeAuction.UnitTests.Helpers;

public class PostgresExceptionHelper
{
    public static DbUpdateException CreateUniqueViolationException()
    {
        var postgresException = new PostgresException(
            "duplicate key value violates unique constraint",
            "",
            "",
            PostgresErrorCodes.UniqueViolation);
    
        var exception = new DbUpdateException(
            "Error saving to database",
            postgresException
        );
    
        return exception;
    }
}