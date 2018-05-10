using Xamarin.Forms;
using FormsApp30.Models;

namespace FormsApp30.DataTemplateSelectors
{
    public class TodoTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PrimaryItemTemplate { get; set; }
        public DataTemplate SecondaryItemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is Todo todo))
                return null;

            return todo.IsDone ? this.SecondaryItemTemplate : this.PrimaryItemTemplate;
        }
    }
}
