using Splat;
using ReactiveUI;

namespace FormsApp18.ViewModels
{
    public class ViewModelBase : ReactiveObject, IRoutableViewModel, ISupportsActivation
    {
        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();

        #region == Implement members of the IRoutableViewModel interface ==

        public string UrlPathSegment { get; protected set; }
        public IScreen HostScreen { get; protected set; }

        #endregion

        #region == Implement members of the ISupportActivation interface ==
        public ViewModelActivator Activator
        {
            get { return viewModelActivator; }
        } 
        #endregion

        protected ViewModelBase(IScreen hostScreen)
        {
            this.HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
