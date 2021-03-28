using Caliburn.Micro;
using PMDesktopUI.Library.API;
using PMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMDesktopUI.ViewModels
{
    public class UserAdministrationViewModel : Screen
    {
        private StatusInfoViewModel _statusInfoViewModel;
        private IWindowManager _windowManager;
        private IUsersEndpoint _userEndPoint;
        private BindingList<UserModel> _users;

        public BindingList<UserModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        public UserAdministrationViewModel(StatusInfoViewModel statusInfoViewModel, IWindowManager windowManager, IUsersEndpoint userEndPoint)
        {
            _statusInfoViewModel = statusInfoViewModel;
            _windowManager = windowManager;
            _userEndPoint = userEndPoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                _statusInfoViewModel.UpdateMessage("Error", ex.Message);
                _windowManager.ShowDialog(_statusInfoViewModel, settings: settings);

                TryClose();
            }
        }

        private async Task LoadUsers()
        {
            var userList = await _userEndPoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }
    }
}