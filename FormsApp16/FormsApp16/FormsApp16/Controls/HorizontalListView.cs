using System.Collections;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FormsApp16.Controls
{
    public class HorizontalListView : View
    {
        #region == Bindable properties ==
        public static readonly BindableProperty ItemsSourceProperty
            = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(HorizontalListView)
                , default(IEnumerable<object>), BindingMode.TwoWay);
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty
            = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(HorizontalListView)
                , default(DataTemplate));
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemWidthProperty
            = BindableProperty.Create(nameof(ItemWidth), typeof(int), typeof(HorizontalListView), default(int));
        public int ItemWidth
        {
            get { return (int)this.GetValue(ItemWidthProperty); }
            set { this.SetValue(ItemWidthProperty, value); }
        }

        public static readonly BindableProperty ItemHeightProperty
            = BindableProperty.Create(nameof(ItemHeight), typeof(int), typeof(HorizontalListView), default(int));
        public int ItemHeight
        {
            get { return (int)this.GetValue(ItemHeightProperty); }
            set { this.SetValue(ItemHeightProperty, value); }
        } 
        #endregion
    }
}
