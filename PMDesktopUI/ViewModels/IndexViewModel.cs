using Caliburn.Micro;
using PMDesktopUI.Helpers;
using PMDesktopUI.Library.API;
using PMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        private bool _isAdding;
        private bool _isMessageToUserVisible;
        private string _messageToUser;

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
            await LoadPasswords();
        }

        private async Task LoadApplications()
        {
            var applicationModels = await _applicationsEndPoint.GetAll();

            Applications = new BindingList<ApplicationModel>(applicationModels);
            ApplicationsAlias = Applications;
        }

        private async Task LoadPasswords()
        {
            var passwordModels = await _passwordsEndPoint.GetAll();
            Passwords = new BindingList<PasswordModel>(passwordModels);
        }

        public BindingList<ApplicationModel> Applications
        {
            get 
            { 
                return _applications?.OrderBy(a => a?.ApplicationAlias).ToList().ToBindingList(); 
            }
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
                try 
                { 
                    SelectedPassword = Passwords?.First(); 
                }
                catch (Exception ex) 
                { 
                    // LOG IT
                }
            }
        }

        public BindingList<PasswordModel> Passwords
        {
            get 
            {
                try
                {
                    return _passwords.Where(p => p.ApplicationId == SelectedApplication?.Id).OrderBy(p => p.PasswordAlias).ToList().ToBindingList();
                }
                catch (ArgumentNullException ex) 
                {
                    // LOG IT
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
                NotifyOfPropertyChange(() => CanEditPassword);
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
                NotifyOfPropertyChange(() => CanAddNewPassword);
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

        public string MessageToUser
        {
            get { return _messageToUser; }
            set
            {
                _messageToUser = value;
                NotifyOfPropertyChange(() => MessageToUser);
            }
        }

        public bool IsMessageToUserVisible
        {
            get { return _isMessageToUserVisible; }
            set
            {
                _isMessageToUserVisible = value;
                NotifyOfPropertyChange(() => IsMessageToUserVisible);
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
                NotifyOfPropertyChange(() => CanCancelChanges);

                NotifyOfPropertyChange(() => AreListsEnabled);
                NotifyOfPropertyChange(() => AreFieldsActive);
            }
        }

        public bool IsAdding
        {
            get { return _isAdding; }
            set 
            {
                _isAdding = value;
                NotifyOfPropertyChange(() => IsAdding);
                NotifyOfPropertyChange(() => CanAddNewApplication);
                NotifyOfPropertyChange(() => CanCopyUsername);
                NotifyOfPropertyChange(() => CanCopyPassword);
                NotifyOfPropertyChange(() => CanEditPassword);
                NotifyOfPropertyChange(() => CanSaveChanges);
                NotifyOfPropertyChange(() => CanAddNewPassword);
                NotifyOfPropertyChange(() => CanDeletePassword);
                NotifyOfPropertyChange(() => CanCancelChanges);

                NotifyOfPropertyChange(() => AreListsEnabled);
                NotifyOfPropertyChange(() => AreFieldsActive);
            }
        }

        public bool AreListsEnabled
        {
            get
            {
                return !IsEditing && !IsAdding;
            }
        }

        public bool AreFieldsActive
        {
            get
            {
                return IsEditing || IsAdding;
            }
        }

        public bool CanAddNewApplication
        {
            get
            {
                return IsEditing || IsAdding;
            }
        }

        public bool CanCopyUsername
        {
            get
            {
                return !IsEditing && !IsAdding;
            }
        }

        public bool CanCopyPassword
        {
            get
            {
                return !IsEditing && !IsAdding;
            }
        }

        public bool CanEditPassword
        {
            get
            {
                if (SelectedPassword is null)
                    return false;

                return !IsEditing && !IsAdding;
            }
        }

        public bool CanSaveChanges
        {
            get
            {
                return IsEditing || IsAdding;
            }
        }

        public bool CanAddNewPassword
        {
            get
            {
                return !IsEditing && !IsAdding && SelectedApplication is object;
            }
        }

        public bool CanDeletePassword
        {
            get
            {
                return IsEditing && !IsAdding;
            }
        }

        public bool CanCancelChanges
        {
            get
            {
                return IsEditing || IsAdding;
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
            if (IsEditing)
                await SaveChangesEdit();

            if (IsAdding)
                await SaveChangesAdd();
        }

        public async Task AddNewPassword()
        {
            PasswordModel passwordModel = new PasswordModel()
            {
                Id = -1,
                ApplicationId = SelectedApplication.Id,
                Encrypted = false,
                PasswordAlias = "Default"
            };

            _passwords.Add(passwordModel);
            NotifyOfPropertyChange(() => Passwords);

            SelectedPassword = passwordModel;

            IsAdding = true;
        }

        public async Task DeletePassword()
        {
            int applicationId = SelectedApplicationAlias.Id;
            int passwordId = SelectedPassword.Id;

            await _passwordsEndPoint.DeletePassword(passwordId);

            await LoadPasswords();

            SelectedApplication = Applications.FirstOrDefault(a => a.Id == applicationId);

            IsEditing = false;
        }

        public async Task CancelChanges()
        {
            if (IsEditing)
                await CancelChangesEdit();

            if (IsAdding)
                await CancelChangesAdd();
        }

        async Task SaveChangesAdd()
        {
            if (string.IsNullOrEmpty(PasswordAlias))
            {
                return;
            }

            if (string.IsNullOrEmpty(Username))
            {
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                return;
            }

            PasswordCreateModel passwordCreateModel = new PasswordCreateModel()
            {
                ApplicationId = SelectedApplicationAlias.Id,
                Encrypted = false,
                Password = Password,
                PasswordAlias = PasswordAlias,
                Username = Username
            };

            int applicationId = passwordCreateModel.ApplicationId;
            int newPasswordId = await _passwordsEndPoint.CreateNewPassword(passwordCreateModel);

            await LoadPasswords();

            SelectedApplication = Applications.First(a => a.Id == applicationId);
            SelectedPassword = Passwords.First(p => p.Id == newPasswordId);

            IsAdding = false;
        }

        async Task SaveChangesEdit()
        {
            if (SelectedPassword.ApplicationId == SelectedApplicationAlias.Id &&
                SelectedPassword.PasswordAlias == PasswordAlias.Trim() &&
                SelectedPassword.Username == Username.Trim() &&
                SelectedPassword.Password == Password.Trim())
            {
                IsEditing = false;
                return;
            }

            int applicationId = SelectedApplicationAlias.Id;
            int passwordId = SelectedPassword.Id;

            PasswordUpdateModel passwordUpdateModel = new PasswordUpdateModel()
            {
                ApplicationId = SelectedApplicationAlias.Id,
                PasswordAlias = PasswordAlias.Trim(),
                Username = Username.Trim(),
                Password = Password.Trim(),
                Encrypted = false
            };

            await _passwordsEndPoint.UpdatePassword(passwordId, passwordUpdateModel);

            await LoadPasswords();

            SelectedApplication = Applications.FirstOrDefault(a => a.Id == applicationId);
            SelectedPassword = Passwords.FirstOrDefault(p => p.Id == passwordId);

            IsEditing = false;
        }

        async Task CancelChangesAdd()
        {
            _passwords.Remove(_passwords.First(p => p.Id == -1));

            SelectedApplicationAlias = SelectedApplication;
            SelectedPassword = Passwords?.FirstOrDefault();
            Username = SelectedPassword?.Username;
            Password = SelectedPassword?.Password;
            PasswordAlias = SelectedPassword?.PasswordAlias;

            NotifyOfPropertyChange(() => Passwords);

            IsAdding = false;
        }

        async Task CancelChangesEdit()
        {
            SelectedApplicationAlias = SelectedApplication;
            Username = SelectedPassword?.Username;
            Password = SelectedPassword?.Password;
            PasswordAlias = SelectedPassword?.PasswordAlias;

            IsEditing = false;
        }
    }
}
