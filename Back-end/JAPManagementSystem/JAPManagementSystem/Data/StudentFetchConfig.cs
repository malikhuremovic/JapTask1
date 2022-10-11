using EntityFrameworkPaginate;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.Data
{
    public static class StudentFetchConfig
    {
        public static Filters<Student> filters;
        public static Sorts<Student> sorts; 

        public static void Initialize(string firstName, string lastName, string email, StudentStatus? status, string selectionName, string japProgramName, string sort, bool descending)
        {
            filters = new Filters<Student>();
            filters.Add(!string.IsNullOrEmpty(firstName), s => s.FirstName.Contains(firstName));
            filters.Add(!string.IsNullOrEmpty(lastName), s => s.LastName.Contains(lastName));
            filters.Add(!string.IsNullOrEmpty(email), s => s.Email.Contains(email));
            filters.Add(status.HasValue, s => s.Status.Equals(status));
            filters.Add(!string.IsNullOrEmpty(selectionName), s => s.Selection.Name.Contains(selectionName));
            filters.Add(!string.IsNullOrEmpty(japProgramName), s => s.Selection.JapProgram.Name.Contains(japProgramName));

            sorts = new Sorts<Student>();
            sorts.Add(sort.Equals("firstName"), s => s.FirstName, descending);
            sorts.Add(sort.Equals("lastName"), s => s.LastName, descending);
            sorts.Add(sort.Equals("email"), s => s.Email, descending);
            sorts.Add(status.HasValue, s => s.Status, descending);
            sorts.Add(sort.Equals("selection"), s => s.Selection.Name, descending);
            sorts.Add(sort.Equals("program"), s => s.Selection.JapProgram.Name, descending);
        }

    }
}
