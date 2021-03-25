using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.ViewModels
{
    public class IndexViewModel : Screen
    {
        private BindingList<string> _applications;
        private BindingList<string> _passwords;
        private string _passwordName;
        private string _username;
        private string _password;

        public BindingList<string> Applications
        {
            get { return _applications; }
            set 
            { 
                _applications = value;
                NotifyOfPropertyChange(() => Applications);
            }
        }

        public BindingList<string> Passwords
        {
            get { return _passwords; }
            set
            {
                _passwords = value;
                NotifyOfPropertyChange(() => Passwords);
            }
        }

        public string PasswordName
        {
            get { return _passwordName; }
            set 
            { 
                _passwordName = value;
                NotifyOfPropertyChange(() => PasswordName);
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

        public bool CanCopyUsername
        {
            get
            {
                return true;
            }
        }

        public bool CanCopyPassword
        {
            get
            {
                return true;
            }
        }
    }
}
