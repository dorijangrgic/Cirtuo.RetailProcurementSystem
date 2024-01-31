namespace Cirtuo.RetailProcurementSystem.Application.Common.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
    public QuarterYear CurrentQuarterYear => GetQuarterYear(UtcNow);
    public QuarterYear QuarterYearFrom(DateTime dateTime) => GetQuarterYear(dateTime);

    private QuarterYear GetQuarterYear(DateTime dateTime)
    {
        var quarter = (dateTime.Month - 1) / 3 + 1;
        return new QuarterYear(quarter, dateTime.Year);
    }
}