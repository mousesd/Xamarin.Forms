using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

using FormsApp19.Controls;
using FormsApp19.Droid.Renderers;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRenderer))]
namespace FormsApp19.Droid.Renderers
{
    public class FacebookLoginButtonRenderer : ViewRenderer
    {
        private bool disposed;

        public FacebookLoginButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                if (!(e.NewElement is FacebookLoginButton element))
                    return;

                var control = new LoginButton(this.Context) { LoginBehavior = LoginBehavior.NativeWithFallback };
                control.SetReadPermissions(element.Permission);
                control.RegisterCallback(MainActivity.CallbackManager
                    , new FacebookCallback(this.Element as FacebookLoginButton));

                this.SetNativeControl(control);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                if (this.Control != null)
                {
                    ((LoginButton)this.Control).UnregisterCallback(MainActivity.CallbackManager);
                    this.Control.Dispose();
                }
                this.disposed = true;
            }
            base.Dispose(disposing);
        }

        private class FacebookCallback : Java.Lang.Object, IFacebookCallback
        {
            private readonly FacebookLoginButton element;

            public FacebookCallback(FacebookLoginButton element)
            {
                this.element = element;
            }

            public void OnCancel()
            {
                element.OnCancel?.Execute(null);
            }

            public void OnError(FacebookException error)
            {
                element.OnError?.Execute(error.Message);
            }

            public void OnSuccess(Java.Lang.Object result)
            {
                element.OnSuccess?.Execute(((LoginResult)result).AccessToken.Token);
            }
        }
    }
}
