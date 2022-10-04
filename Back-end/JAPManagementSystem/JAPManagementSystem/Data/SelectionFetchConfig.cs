using EntityFrameworkPaginate;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Data
{
    public class SelectionFetchConfig
    {
        public static Filters<Selection> filters = new Filters<Selection>();
        public static Sorts<Selection> sorts = new Sorts<Selection>();

        public static void Initialize(string name, SelectionStatus? status, string japProgramName, DateTime? dateStart, DateTime? dateEnd, string sort, bool descending)
        {
            filters.Add(!string.IsNullOrEmpty(name), s => s.Name.Contains(name));
            filters.Add(!string.IsNullOrEmpty(japProgramName), s => s.JapProgram.Name.Contains(japProgramName));
            filters.Add(dateStart != null, s => s.DateStart == dateStart);
            filters.Add(dateEnd != null, s => s.DateEnd == dateEnd);
            filters.Add(status.HasValue, s => s.Status == status);

            sorts.Add(sort.Equals("name"), s => s.Name, descending);
            sorts.Add(sort.Equals("dateStart"), s => s.DateStart, descending);
            sorts.Add(sort.Equals("dateEnd"), s => s.DateEnd, descending);
            sorts.Add(sort.Equals("status"), s => s.Status, descending);
            sorts.Add(sort.Equals("japProgramName"), s => s.JapProgram.Name, descending);

        }
    }
}
