using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FormsApp24.Behaviors
{
    public class EffectBehavior : Behavior<View>
    {
        public static readonly BindableProperty GroupProperty
            = BindableProperty.Create(nameof(Group), typeof(string), typeof(EffectBehavior));
        public string Group
        {
            get { return (string)this.GetValue(GroupProperty); }
            set { this.SetValue(GroupProperty, value); }
        }

        public static readonly BindableProperty NameProperty
            = BindableProperty.Create(nameof(Name), typeof(string), typeof(EffectBehavior));
        public string Name
        {
            get { return (string)this.GetValue(NameProperty); }
            set { this.SetValue(NameProperty, value); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            this.AddEffect(bindable);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            this.RemoveEffect(bindable);
            base.OnDetachingFrom(bindable);
        }

        private void AddEffect(View bindable)
        {
            var effect = this.GetEffect();
            if (effect != null)
                bindable.Effects.Add(effect);
        }

        private void RemoveEffect(View bindable)
        {
            var effect = this.GetEffect();
            if (effect != null)
                bindable.Effects.Remove(effect);
        }

        private Effect GetEffect()
        {
            if (!string.IsNullOrEmpty(this.Group) && !string.IsNullOrEmpty(this.Name))
                return Effect.Resolve($"{this.Group}.{this.Name}");
            return null;
        }
    }
}
