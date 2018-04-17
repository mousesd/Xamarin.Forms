using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace FormsApp22.Behaviors
{
    public class PickerColorBehavior : Behavior<Picker>
    {
        #region == Bindable properties ==
        public string[] ValidValues
        {
            get { return (string[])this.GetValue(ValidValuesProperty); }
            set { this.SetValue(ValidValuesProperty, value); }
        }
        public static BindableProperty ValidValuesProperty
            = BindableProperty.Create(nameof(ValidValues), typeof(string[]), typeof(PickerColorBehavior));

        public Color ValidColor
        {
            get { return (Color)this.GetValue(ValidColorProperty); }
            set { this.SetValue(ValidColorProperty, value); }
        }
        public static BindableProperty ValidColorProperty
            = BindableProperty.Create(nameof(ValidColor), typeof(Color), typeof(PickerColorBehavior), Color.Default);

        public Color InvalidColor
        {
            get { return (Color)this.GetValue(InvalidColorProperty); }
            set { this.SetValue(InvalidColorProperty, value); }
        }
        public static BindableProperty InvalidColorProperty
            = BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(PickerColorBehavior), Color.Red);

        public bool IsValid
        {
            get { return (bool)this.GetValue(IsValidProperty); }
            set { this.SetValue(IsValidProperty, value); }
        }
        public static BindableProperty IsValidProperty
            = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(PickerColorBehavior), false);
        #endregion

        #region == Override members of the Behavior<T> class ==
        protected override void OnAttachedTo(Picker bindable)
        {
            bindable.SelectedIndexChanged += this.OnSelectedIndexChanged;
        }

        protected override void OnDetachingFrom(Picker bindable)
        {
            bindable.SelectedIndexChanged -= this.OnSelectedIndexChanged;
        } 
        #endregion

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //# Bound and cast to a picker
            if (!(sender is Picker bindable))
                return;

            //# Make sure the picker is data bound
            if (!(bindable.ItemDisplayBinding is Binding displayBinding))
                return;

            //# Get the binding path
            string displayBindingPath = displayBinding.Path;

            //# Use reflection to get the value of the selected itme of the picker
            var selectedItem = bindable.SelectedItem.GetType().GetRuntimeProperty(displayBindingPath);
            var selectedText = selectedItem.GetValue(bindable.SelectedItem);

            //# Check to see if everything is valid
            if (this.ValidValues != null && this.ValidValues.Contains(selectedText))
            {
                this.IsValid = true;
                bindable.BackgroundColor = this.ValidColor;
            }
            else
            {
                this.IsValid = false;
                bindable.BackgroundColor = this.InvalidColor;
            }
        }
    }
}
