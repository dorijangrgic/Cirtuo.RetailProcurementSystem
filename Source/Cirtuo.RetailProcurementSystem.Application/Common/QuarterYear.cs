namespace Cirtuo.RetailProcurementSystem.Application.Common;

public class QuarterYear
{
    public int Quarter { get; set; }
    public int Year { get; set; }
    
    public QuarterYear(int quarter, int year)
    {
        Quarter = quarter;
        Year = year;
    }
    
    public DateTime StartDate => new DateTime(Year, Quarter * 3 - 2, 1).ToUniversalTime();
    public DateTime EndDate => StartDate.AddMonths(3);
    
    public QuarterYear Next()
    {
        return Quarter == 4 ?
            new QuarterYear(1, Year + 1) :
            new QuarterYear(Quarter + 1, Year);
    }
    
    public bool Equals(QuarterYear other)
    {
        return Quarter == other.Quarter && Year == other.Year;
    }
}