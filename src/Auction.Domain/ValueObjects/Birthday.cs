using Auction.Exceptions.Exceptions;

namespace Auction.Domain.ValueObjects;

public record Birthday
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
    
    public int Age => DateTime.UtcNow.Year - Year;
    
    protected Birthday() { }

    private Birthday(
        int year, 
        int month, 
        int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    public static Birthday Create(int year, int month, int day)
    {
        if (year < 0)
            throw ErrorExceptions.Negative<Birthday>(nameof(year));

        if (month < 1 || month > 12)
            throw ErrorExceptions.ValueOutsideBounds<Birthday>(nameof(month), 1, 12);
        
        if (day < 1 || day > 31)
            throw ErrorExceptions.ValueOutsideBounds<Birthday>(nameof(day), 1, 31);

        return new Birthday(year, month, day);
    }
}