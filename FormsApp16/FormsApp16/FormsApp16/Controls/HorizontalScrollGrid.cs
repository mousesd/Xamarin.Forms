using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FormsApp16.Controls
{
    public class HorizontalScrollGrid : Grid
    {
        #region == Events, Fields & Properties ==
        public event EventHandler SelectedItemChanged;

        private readonly ScrollView scrollView;
        private readonly StackLayout itemsLayout;

        public double Spaceing { get; set; }
        public StackOrientation ListOrientation { get; set; }

        private ICommand innerItemSelectedCommand; 
        #endregion

        #region == Bindable properties ==
        public static readonly BindableProperty SelectedCommandProperty
            = BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(HorizontalScrollGrid));
        public ICommand SelectedCommand
        {
            get { return (ICommand)this.GetValue(SelectedCommandProperty); }
            set { this.SetValue(SelectedCommandProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty
            = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(HorizontalScrollGrid)
                , default(IEnumerable<object>), BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty
            = BindableProperty.Create("SelectedItem", typeof(object), typeof(HorizontalScrollGrid), null
                , BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);
        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty
            = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(HorizontalScrollGrid)
                , default(DataTemplate));
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        } 
        #endregion

        #region == Constructors ==
        public HorizontalScrollGrid()
        {
            this.scrollView = new ScrollView
            {
                BackgroundColor = this.BackgroundColor,
                Content = itemsLayout = new StackLayout
                {
                    BackgroundColor = this.BackgroundColor,
                    Padding = this.Padding,
                    Spacing = this.Spaceing,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                }
            };
            this.Children.Add(this.scrollView);
        }
        #endregion

        #region == Methods ==
        private static void ItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is HorizontalScrollGrid itemsLayout))
                return;

            itemsLayout.SetItems();
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is HorizontalScrollGrid itemsLayout))
                return;

            if (newValue == oldValue && newValue != null)
                return;

            itemsLayout.SelectedItemChanged?.Invoke(itemsLayout, EventArgs.Empty);
            if (itemsLayout.SelectedCommand?.CanExecute(newValue) ?? false)
                itemsLayout.SelectedCommand?.Execute(newValue);
        }

        protected virtual void SetItems()
        {
            itemsLayout.Children.Clear();
            itemsLayout.Spacing = this.Spaceing;
            innerItemSelectedCommand = new Command<View>(v =>
            {
                this.SelectedItem = v.BindingContext;
                this.SelectedItem = null;
            });

            itemsLayout.Orientation = this.ListOrientation;
            scrollView.Orientation = this.ListOrientation == StackOrientation.Horizontal
                ? ScrollOrientation.Horizontal : ScrollOrientation.Vertical;

            if (this.ItemsSource == null)
                return;

            foreach (var item in this.ItemsSource)
                itemsLayout.Children.Add(this.GetItemView(item));

            itemsLayout.BackgroundColor = this.BackgroundColor;
            this.SelectedItem = null;
        }

        protected virtual View GetItemView(object item)
        {
            var content = this.ItemTemplate.CreateContent();
            if (!(content is View view))
                return null;

            view.BindingContext = item;
            var gesture = new TapGestureRecognizer
            {
                Command = innerItemSelectedCommand,
                CommandParameter = view
            };

            this.AddGesture(view, gesture);
            return view;
        }

        private void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);
            if (!(view is Layout<View> layout))
                return;

            foreach (var child in layout.Children)
                this.AddGesture(child, gesture);
        } 
        #endregion
    }
}
