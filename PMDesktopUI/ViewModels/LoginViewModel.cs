using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using PMDesktopUI.EventModels;
using PMDesktopUI.Library.API;
using PMDesktopUI.Library.Helpers;

namespace PMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private string _errorMessage;
        private bool _activeControls;

        private IAPIHelper apiHelper;
        private IEventAggregator events;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events, IConfigHelper configHelper)
        {
            this.apiHelper = apiHelper;
            this.events = events;
            _activeControls = true;
            UserName = configHelper.GetDefaultUsername();
            Password = configHelper.GetDefaultPassword();
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

                await apiHelper.GetLoggedInUserInfo(result.Access_Token);

                events.PublishOnUIThread(new LogOnEventModel());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            ActiveControls = true;
        }
    }
}
