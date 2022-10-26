using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAPManagenent.Utils.Util
{
    public interface IDateCalculator
    {
        void CalculateTimeDifference(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
        void CalculateTimeDifferenceWithWorkingHours(DateTime previousEndDate, int expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);

    }
}
