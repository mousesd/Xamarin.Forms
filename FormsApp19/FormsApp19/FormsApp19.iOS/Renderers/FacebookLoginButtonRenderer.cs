using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Facebook.LoginKit;

using FormsApp19.Controls;

using FormsApp19.iOS.Renderers;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRenderer))]
namespace FormsApp19.iOS.Renderers
{
    public class FacebookLoginButtonRenderer : ViewRenderer
    {
        private bool disposed;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                if (!(e.NewElement is FacebookLoginButton element))
                    return;
                var control = new LoginButton
                {
                    LoginBehavior = LoginBehavior.Native,
                    ReadPermissions = element.Permission
                };

                control.Completed += this.AuthCompleted;
                this.SetNativeControl(control);
            }
        }

        private void AuthCompleted(object sender, LoginButtonCompletedEventArgs e)
        {
            if (!(this.Element is FacebookLoginButton element))
                return;

            if (e.Error != null)
                element.OnError?.Execute(e.Error.ToString());
            else if (e.Result.IsCancelled)
                element.OnCancel?.Execute(null);
            else
                element.OnSuccess?.Execute(e.Result.Token.TokenString);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                if (this.Control != null)
                {
                    ((LoginButton)this.Control).Completed -= this.AuthCompleted;
                    this.Control.Dispose();
                }
                this.disposed = true;
            }
            base.Dispose(disposing);
        }
    }
}
