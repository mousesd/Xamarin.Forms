using System;
using Xamarin.Forms;

namespace FormsApp31.Behaviors
{
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        #region == Override members of the Behavior<T> class ==
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            this.AssociatedObject = bindable;
            if (bindable.BindingContext != null)
                this.BindingContext = bindable.BindingContext;
            bindable.BindingContextChanged += this.OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= this.OnBindingContextChanged;
            this.AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        } 
        #endregion

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            this.OnBindingContextChanged();
        }
    }
}
