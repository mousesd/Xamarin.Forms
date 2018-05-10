using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace FormsApp30.Behaviors
{
    public class EventToCommandBehavior : BehaviorBase<View>
    {
        private Delegate eventHandler;

        #region == Bindable properties ==
        /// <summary>
        /// The name of the event the behavior listens to.
        /// </summary>
        public static readonly BindableProperty EventNameProperty
            = BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), null
                , propertyChanged: OnEventNameChanged);
        public string EventName
        {
            get { return (string)this.GetValue(EventNameProperty); }
            set { this.SetValue(EventNameProperty, value); }
        }

        /// <summary>
        /// The ICommand to be executed, The behavior expects to find the ICommand instance on the BindingContext of 
        /// the attached control, which may be inherited from a parent element.
        /// </summary>
        public static readonly BindableProperty CommandProperty
            = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// An object that will be passed to the command.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty
            = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));
        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// An IValueConverter implementation that will change the format of the event argument data as it's passed
        /// between source and target by the binding engine.
        /// </summary>
        public static readonly BindableProperty ConverterProperty
            = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(EventToCommandBehavior));
        public IValueConverter Converter
        {
            get { return (IValueConverter)this.GetValue(ConverterProperty); }
            set { this.SetValue(ConverterProperty, value); }
        }
        #endregion

        #region == Override members of the BehaviorBase<T> class ==
        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            this.RegisterEvent(this.EventName);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            this.DeregisterEvent(this.EventName);
            base.OnDetachingFrom(bindable);
        }
        #endregion

        private static void OnEventNameChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var behavior = (EventToCommandBehavior)bindable;
            if (behavior.AssociatedObject == null)
                return;

            string oldEventName = (string)oldvalue;
            string newEventName = (string)newvalue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }

        private void RegisterEvent(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
                return;

            var eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
                throw new ArgumentException($"{nameof(EventToCommandBehavior)}: Can't register the '{eventName}' event.");

            var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(this.AssociatedObject, eventHandler);
        }

        private void DeregisterEvent(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
                return;

            if (this.AssociatedObject == null)
                return;

            var eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
                throw new ArgumentException($"{nameof(EventToCommandBehavior)}: Can't de-register the '{eventName}' event.");

            eventInfo.RemoveEventHandler(this.AssociatedObject, eventHandler);
            eventHandler = null;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private void OnEvent(object sender, object e)
        {
            if (this.Command == null)
                return;

            object resolvedParameter;
            if (this.CommandParameter != null)
                resolvedParameter = this.CommandParameter;
            else if (this.Converter != null)
                resolvedParameter = this.Converter.Convert(e, typeof(object), null, null);
            else
                resolvedParameter = e;

            if (this.Command.CanExecute(resolvedParameter))
                this.Command.Execute(resolvedParameter);
        }
    }
}
