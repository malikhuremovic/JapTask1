using EntityFrameworkPaginate;
using JAPManagementSystem.Models;
using JAPManagementSystem.Models.SelectionModel;

namespace JAPManagementSystem.Data
{
    public class ItemFetchConfig
    {
        public static Filters<Item> filters;
        public static Sorts<Item> sorts;

        public static void Initialize(string name, string description, string URL, int? expectedHours, string sort, bool descending)
        {
            filters = new Filters<Item>();
            filters.Add(!string.IsNullOrEmpty(name), l => l.Name.Contains(name));
            filters.Add(!string.IsNullOrEmpty(description), l => l.Description.Contains(description));
            filters.Add(!string.IsNullOrEmpty(URL), l => l.URL.Contains(description));
            filters.Add(expectedHours != null, l => l.ExpectedHours == expectedHours);

            sorts = new Sorts<Item>();
            sorts.Add(sort.Equals("name"), l => l.Name, descending);
            sorts.Add(sort.Equals("description"), l => l.Description, descending);
            sorts.Add(sort.Equals("URL"), l => l.URL, descending);
            sorts.Add(sort.Equals("expectedHours"), l => l.ExpectedHours, descending);
        }
    }
}
