namespace DateCalculation.Util
{
    public class DateCalculator : IDateCalculator
    {
        public static int workingHoursPerDay = 8;
        public void CalculateTimeDifference(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate)
        {
            newStartDate = previousEndDate;
            newEndDate = previousEndDate.AddHours(expectedHoursToComplete);
        }
        public void CalculateTimeDifferenceWithWorkingHours(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate)
        {
            int daysToAdd = (int)(expectedHoursToComplete / workingHoursPerDay);
            int hoursToAdd = expectedHoursToComplete % workingHoursPerDay;
            newStartDate = previousEndDate;
            newEndDate = previousEndDate.AddDays(daysToAdd).AddHours(hoursToAdd);
            if (newEndDate.DayOfWeek == DayOfWeek.Sunday)
            {
                newEndDate = newEndDate.AddDays(1);
            }
            if (newEndDate.DayOfWeek == DayOfWeek.Saturday)
            {
                newEndDate = newEndDate.AddDays(2);
            }
        }
    }
}