using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PMDesktopUI.EventModels;
using PMDesktopUI.Library.Models;

namespace PMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>
    {
        private IndexViewModel _indexViewModel;
        private IEventAggregator _events;
        private ILoggedInUserModel _user;

        public ShellViewModel(IEventAggregator events, IndexViewModel indexViewModel, ILoggedInUserModel user)
        {
            _events = events;
            _indexViewModel = indexViewModel;
            _user = user;

            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEventModel message)
        {
            _indexViewModel = IoC.Get<IndexViewModel>();
            ActivateItem(_indexViewModel);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
        
        public async Task LoginScreen()
        {
            _user.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public async Task Index()
        {
            ActivateItem(_indexViewModel);
        }

        public async Task UserManagement()
        {
            ActivateItem(IoC.Get<UserAdministrationViewModel>());
        }

        public async Task ExitApplication()
        {
            TryClose();
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(_user.Token);
            }
        }
    }
}
