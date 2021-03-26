using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.Models
{
    public class PasswordCreateModel
    {
        public int ApplicationId { get; set; }
        public string PasswordAlias { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Encrypted { get; set; }
    }
}
