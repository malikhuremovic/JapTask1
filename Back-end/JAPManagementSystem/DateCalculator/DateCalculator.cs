namespace DateCalculation
{
    public class DateCalculator
    {
        public void CalculateTimeDifference(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate)
        {
            newStartDate = previousEndDate;
            newEndDate = previousEndDate.AddHours(expectedHoursToComplete);
        }
    }
}