using Xamarin.Forms;

namespace FormsApp19.Controls
{
    public class FacebookLoginButton : View
    {
        /// <summary>
        /// Array of permission to be asked from the Facebook user account
        /// </summary>
        public string[] Permission
        {
            get { return (string[])this.GetValue(PermissionProperty); }
            set { this.SetValue(PermissionProperty, value); }
        }
        public static readonly BindableProperty PermissionProperty =
            BindableProperty.Create(nameof(Permission), typeof(string[]), typeof(FacebookLoginButton)
                , new[] { "public_profile", "email" });

        /// <summary>
        /// A command that will return the authentication token and execute when the authentication process will complete
        /// </summary>
        public Command<string> OnSuccess
        {
            get { return (Command<string>)this.GetValue(OnSuccessProperty); }
            set { this.SetValue(OnSuccessProperty, value); }
        }
        public static readonly BindableProperty OnSuccessProperty =
            BindableProperty.Create(nameof(OnSuccess), typeof(Command<string>), typeof(FacebookLoginButton));

        /// <summary>
        /// A command that will return the error description and execute when an exception will be thrown by the authentication process
        /// </summary>
        public Command<string> OnError
        {
            get { return (Command<string>)this.GetValue(OnErrorProperty); }
            set { this.SetValue(OnErrorProperty, value); }
        }
        public static readonly BindableProperty OnErrorProperty =
            BindableProperty.Create(nameof(OnError), typeof(Command<string>), typeof(FacebookLoginButton));

        /// <summary>
        /// A command that will execute when the user will manually cancel the authentication process
        /// </summary>
        public Command OnCancel
        {
            get { return (Command)this.GetValue(OnCancelProperty); }
            set { this.SetValue(OnCancelProperty, value); }
        }
        public static readonly BindableProperty OnCancelProperty =
            BindableProperty.Create(nameof(OnCancel), typeof(Command), typeof(FacebookLoginButton));
    }
}
