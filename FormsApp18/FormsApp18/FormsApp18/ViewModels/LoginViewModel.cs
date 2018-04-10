using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using FormsApp18.Services;
using ReactiveUI;

namespace FormsApp18.ViewModels
{
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "RedundantLogicalConditionalExpressionOperand")]
    public class LoginViewModel : ViewModelBase
    {
        #region == Fields & Properties ==
        private readonly ILogin loginService;

        public string email;
        public string Email
        {
            get { return email; }
            set { this.RaiseAndSetIfChanged(ref email, value); }
        }

        public string password;
        public string Password
        {
            get { return password; }
            set { this.RaiseAndSetIfChanged(ref password, value); }
        }

        private ObservableAsPropertyHelper<bool> _validLogin;
        public bool ValidLogin
        {
            get { return _validLogin?.Value ?? false; }
        } 
        #endregion

        public ReactiveCommand LoginCommand { get; }

        public LoginViewModel(ILogin loginService, IScreen hostScreen = null) : base(hostScreen)
        {
            this.loginService = loginService;
            this.WhenAnyValue(x => x.Email, x => x.Password
                    , (email, password) =>
                        !string.IsNullOrEmpty(password)
                        && password.Length > 2
                        && !string.IsNullOrEmpty(email)
                        && Regex.Matches(email, "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Count == 1)
                .ToProperty(this, v => v.ValidLogin, out _validLogin);

            this.LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                bool isLogin = await loginService.Login(email, password);
                if (isLogin)
                    HostScreen.Router.Navigate.Execute(new ItemsViewModel()).Subscribe();
            }
            , this.WhenAnyValue(x => x.ValidLogin, validLogin => validLogin && true));
        }
    }
}
