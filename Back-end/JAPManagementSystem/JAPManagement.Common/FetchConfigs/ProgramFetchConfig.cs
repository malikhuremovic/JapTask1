using EntityFrameworkPaginate;
using JAPManagement.Core.Models.ProgramModel;

namespace JAPManagement.Common.FetchConfigs
{
    public class ProgramFetchConfig
    {
        public static Filters<JapProgram> filters;
        public static Sorts<JapProgram> sorts;

        public static void Initialize(string name, string content, string sort, bool descending)
        {
            filters = new Filters<JapProgram>();
            filters.Add(!string.IsNullOrEmpty(name), l => l.Name.Contains(name));
            filters.Add(!string.IsNullOrEmpty(content), l => l.Content.Contains(content));

            sorts = new Sorts<JapProgram>();
            sorts.Add(sort.Equals("name"), l => l.Name, descending);
            sorts.Add(sort.Equals("content"), l => l.Content, descending);
        }
    }
}
