using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FormsApp22.Behaviors
{
    public class EntryPressBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty CommandProperty
            = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EntryPressBehavior), null);
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable.BindingContext != null)
                this.BindingContext = bindable.BindingContext;

            bindable.BindingContextChanged += this.Bindable_BindingContextChanged;
            bindable.TextChanged += this.Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= this.Bindable_BindingContextChanged;
            bindable.TextChanged -= this.Bindable_TextChanged;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            base.OnBindingContextChanged();

            if (!(sender is BindableObject bindable))
                return;

            this.BindingContext = bindable.BindingContext;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Command?.Execute(e.NewTextValue);
        }
    }
}
