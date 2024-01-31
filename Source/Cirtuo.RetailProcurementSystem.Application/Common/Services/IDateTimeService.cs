namespace Cirtuo.RetailProcurementSystem.Application.Common.Services;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
    QuarterYear CurrentQuarterYear { get; }
    QuarterYear QuarterYearFrom(DateTime dateTime);
}