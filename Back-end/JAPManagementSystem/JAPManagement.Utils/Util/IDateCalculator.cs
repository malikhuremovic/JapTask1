using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.Models.StudentModel;

namespace JAPManagenent.Utils.Util
{
    public interface IDateCalculator
    {
        List<AddStudentItemDto> CalculateStartAndEndDate(Student student, List<AddStudentItemDto> studentItemList);
        void CalculateTimeDifference(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
        void CalculateTimeDifferenceWithWorkingHours(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);

    }
}
