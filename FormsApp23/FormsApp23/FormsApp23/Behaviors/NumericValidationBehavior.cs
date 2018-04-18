using Xamarin.Forms;

namespace FormsApp23.Behaviors
{
    public static class NumericValidationBehavior
    {
        public static readonly BindableProperty AttachBehaviorProperty
            = BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(NumericValidationBehavior)
                , false, propertyChanged: OnAttachBehaviorChange);

        public static bool GetAttachBehavior(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject bindable, bool value)
        {
            bindable.SetValue(AttachBehaviorProperty, value);
        }

        private static void OnAttachBehaviorChange(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is Entry entry))
                return;

            bool attachBehavior = (bool)newvalue;
            if (attachBehavior)
                entry.TextChanged += OnEntryTextChanged;
            else
                entry.TextChanged -= OnEntryTextChanged;
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValid = double.TryParse(e.NewTextValue, out _);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
