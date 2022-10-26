namespace DateCalculation.Util
{
    public class DateCalculator : IDateCalculator
    {
        public void CalculateTimeDifference(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate)
        {
            newStartDate = previousEndDate;
            newEndDate = previousEndDate.AddHours(expectedHoursToComplete);
        }
    }
}