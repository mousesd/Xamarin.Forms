using System.Collections.Generic;

namespace FormsApp30.Services
{
    public class ListViewGrouping<T> : List<T>
    {
        public string Title { get; set; }
        public string ShortName { get; set; }

        public ListViewGrouping(string title, string shortName)
        {
            this.Title = title;
            this.ShortName = shortName;
        }
    }
}
