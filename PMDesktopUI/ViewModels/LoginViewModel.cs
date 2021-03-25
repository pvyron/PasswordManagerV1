using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using PMDesktopUI.Library.API;

namespace PMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private string _errorMessage;
        private bool _activeControls;

        private IAPIHelper apiHelper;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            this.apiHelper = apiHelper;
            _activeControls = true;
        }

        public string UserName
        {
            get { return _userName; }
            set 
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                    output = true;

                return ActiveControls && output;
            }
        }

        public bool ActiveControls
        {
            get { return _activeControls; }
            set 
            {
                _activeControls = value;
                NotifyOfPropertyChange(() => ActiveControls);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public async Task LogIn()
        {
            ActiveControls = false;
            ErrorMessage = "";

            try
            {
                var result = await apiHelper.Authenticate(UserName, Password);

                // Capture more info for the user
                await apiHelper.GetLoggedInUserInfo(result.Access_Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            ActiveControls = true;
        }
    }
}
