using System;
using System.Collections.Generic;
using System.Text;
using FormsApp18.Services;
using FormsApp18.ViewModels;
using FormsApp18.Views;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace FormsApp18
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        public AppBootstrapper()
        {
            this.Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            Locator.CurrentMutable.Register(() => new LoginService(), typeof(ILogin));
            Locator.CurrentMutable.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
            Locator.CurrentMutable.Register(() => new ItemPage(), typeof(IViewFor<ItemsViewModel>));

            this.Router.NavigateAndReset
                .Execute(new LoginViewModel(Locator.CurrentMutable.GetService<ILogin>()))
                .Subscribe();
        }

        public Page CreateMainPage()
        {
            return new ReactiveUI.XamForms.RoutedViewHost();
        }
    }
}
