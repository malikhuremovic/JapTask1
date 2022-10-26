using EntityFrameworkPaginate;
using JAPManagement.Core.Models.ProgramModel;

namespace JAPManagement.Common
{
    public class ItemFetchConfig
    {
        public static Filters<JapItem> filters;
        public static Sorts<JapItem> sorts;

        public static void Initialize(string name, string description, string URL, int? expectedHours, string? isEvent, string sort, bool descending)
        {
            filters = new Filters<JapItem>();
            filters.Add(!string.IsNullOrEmpty(name), l => l.Name.Contains(name));
            filters.Add(!string.IsNullOrEmpty(description), l => l.Description.Contains(description));
            filters.Add(!string.IsNullOrEmpty(URL), l => l.URL.Contains(URL));
            filters.Add(expectedHours != null, l => l.ExpectedHours == expectedHours);
            filters.Add(isEvent != null && isEvent.Equals("true"), l => l.IsEvent == bool.Parse(isEvent));

            sorts = new Sorts<JapItem>();
            sorts.Add(sort.Equals("name"), l => l.Name, descending);
            sorts.Add(sort.Equals("description"), l => l.Description, descending);
            sorts.Add(sort.Equals("url"), l => l.URL, descending);
            sorts.Add(sort.Equals("expectedHours"), l => l.ExpectedHours, descending);
            sorts.Add(sort.Equals("isEvent"), l => l.IsEvent, descending);

        }
    }
}
