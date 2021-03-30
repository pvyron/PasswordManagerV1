using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }

        public async Task HandleAsync(LogOnEventModel message, CancellationToken cancellationToken)
        {
            _indexViewModel = IoC.Get<IndexViewModel>();
            await ActivateItemAsync(_indexViewModel, cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
        
        public async Task LoginScreen()
        {
            _user.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public async Task Index()
        {
            await ActivateItemAsync(_indexViewModel);
        }

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserAdministrationViewModel>());
        }

        public async Task ExitApplication()
        {
            await TryCloseAsync();
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
