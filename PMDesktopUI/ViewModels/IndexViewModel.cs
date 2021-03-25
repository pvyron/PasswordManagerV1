using Caliburn.Micro;
using PMDesktopUI.Helpers;
using PMDesktopUI.Library.API;
using PMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMDesktopUI.ViewModels
{
    public class IndexViewModel : Screen
    {
        private BindingList<ApplicationModel> _applications;
        private ApplicationModel _selectedApplication;
        private BindingList<PasswordModel> _passwords;
        private PasswordModel _selectedPassword;
        private BindingList<ApplicationModel> _applicationsAlias;
        private ApplicationModel _selectedApplicationsAlias;
        private string _passwordAlias;
        private string _username;
        private string _password;
        private bool _isEditing;

        private IApplicationsEndPoint _applicationsEndPoint;
        private IPasswordsEndPoint _passwordsEndPoint;

        public IndexViewModel(IApplicationsEndPoint applicationsEndPoint, IPasswordsEndPoint passwordsEndPoint)
        {
            _applicationsEndPoint = applicationsEndPoint;
            _passwordsEndPoint = passwordsEndPoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            await LoadApplications();
        }

        private async Task LoadApplications()
        {
            var applicationModels = await _applicationsEndPoint.GetAll();
            var passwordModels = await _passwordsEndPoint.GetAll();

            Applications = new BindingList<ApplicationModel>(applicationModels);
            ApplicationsAlias = Applications;
            Passwords = new BindingList<PasswordModel>(passwordModels);
        }

        public BindingList<ApplicationModel> Applications
        {
            get { return _applications; }
            set 
            { 
                _applications = value;
                NotifyOfPropertyChange(() => Applications);
            }
        }

        public ApplicationModel SelectedApplication
        {
            get { return _selectedApplication; }
            set 
            {
                _selectedApplication = value;
                NotifyOfPropertyChange(() => SelectedApplication);
                NotifyOfPropertyChange(() => Passwords);

                SelectedApplicationAlias = _selectedApplication;
                try { SelectedPassword = Passwords?.First(); }
                catch (Exception ex) { }
            }
        }

        public BindingList<PasswordModel> Passwords
        {
            get 
            {
                try
                {
                    return _passwords.Where(p => p.ApplicationId == SelectedApplication?.Id).ToList().ToBindingList();
                }
                catch (ArgumentNullException ex) 
                {
                    return new BindingList<PasswordModel>();
                }
            }
            set
            {
                _passwords = value;
                NotifyOfPropertyChange(() => Passwords);
            }
        }

        public PasswordModel SelectedPassword 
        {
            get 
            { 
                return _selectedPassword; 
            }
            set
            {
                _selectedPassword = value;

                Username = _selectedPassword?.Username;
                Password = _selectedPassword?.Password;
                PasswordAlias = _selectedPassword?.PasswordAlias;
                NotifyOfPropertyChange(() => SelectedPassword);
            } 
        }

        public BindingList<ApplicationModel> ApplicationsAlias
        {
            get { return _applicationsAlias; }
            set 
            { 
                _applicationsAlias = value;
                NotifyOfPropertyChange(() => ApplicationsAlias);
            }
        }

        public ApplicationModel SelectedApplicationAlias
        {
            get { return _selectedApplicationsAlias; }
            set 
            { 
                _selectedApplicationsAlias = value;
                NotifyOfPropertyChange(() => SelectedApplicationAlias);
            }
        }

        public string PasswordAlias
        {
            get { return _passwordAlias; }
            set 
            { 
                _passwordAlias = value;
                NotifyOfPropertyChange(() => PasswordAlias);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                NotifyOfPropertyChange(() => IsEditing);
                NotifyOfPropertyChange(() => CanAddNewApplication);
                NotifyOfPropertyChange(() => CanCopyUsername);
                NotifyOfPropertyChange(() => CanCopyPassword);
                NotifyOfPropertyChange(() => CanEditPassword);
                NotifyOfPropertyChange(() => CanSaveChanges);
                NotifyOfPropertyChange(() => CanAddNewPassword);
                NotifyOfPropertyChange(() => CanDeletePassword);
            }
        }

        public bool CanAddNewApplication
        {
            get
            {
                return IsEditing;
            }
        }

        public bool CanCopyUsername
        {
            get
            {
                return !IsEditing;
            }
        }

        public bool CanCopyPassword
        {
            get
            {
                return !IsEditing;
            }
        }

        public bool CanEditPassword
        {
            get
            {
                return !IsEditing;
            }
        }

        public bool CanSaveChanges
        {
            get
            {
                return IsEditing;
            }
        }

        public bool CanAddNewPassword
        {
            get
            {
                return !IsEditing;
            }
        }

        public bool CanDeletePassword
        {
            get
            {
                return IsEditing;
            }
        }

        public async Task CopyUsername()
        {
            if (!string.IsNullOrEmpty(Username))
                Clipboard.SetText(Username);
        }

        public async Task CopyPassword()
        {
            if (!string.IsNullOrEmpty(Password))
                Clipboard.SetText(Password);
        }

        public async Task AddNewApplication()
        {

        }

        public async Task EditPassword()
        {
            IsEditing = true;
        }

        public async Task SaveChanges()
        {
            IsEditing = false;
        }

        public async Task AddNewPassword()
        {

        }

        public async Task DeletePassword()
        {

        }
    }
}
