using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.Models.StudentModel;

namespace JAPManagenent.Utils.Util
{
    public class DateCalculator : IDateCalculator
    {
        public static int workingHoursPerDay = 8;
        public List<AddStudentItemDto> CalculateStartAndEndDate(Student student, List<AddStudentItemDto> studentItemList)
        {
            List<AddStudentItemDto> updatedItemList = new List<AddStudentItemDto>();
            for (int i = 0; i < studentItemList.Count; i++)
            {
                var studentItem = studentItemList.ElementAt(i);
                DateTime previousEndDate;
                if (i == 0)
                {
                    previousEndDate = student.Selection.DateStart;
                }
                else
                {
                    previousEndDate = studentItemList.ElementAt(i - 1).EndDate;
                }
                CalculateTimeDifferenceWithWorkingHours(previousEndDate, studentItem.ExpectedHours, out DateTime newStartDate, out DateTime newEndDate);
                studentItem.StartDate = newStartDate;
                studentItem.EndDate = newEndDate;
                updatedItemList.Add(studentItem);
            }
            return updatedItemList;
        }

        public void CalculateTimeDifference(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate)
        {
            newStartDate = previousEndDate;
            newEndDate = previousEndDate.AddHours(expectedHoursToComplete);
        }
        public void CalculateTimeDifferenceWithWorkingHours(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate)
        {
            int daysToAdd = expectedHoursToComplete / workingHoursPerDay;
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