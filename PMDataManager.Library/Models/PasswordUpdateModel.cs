using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataManager.Library.Models
{
    public class PasswordUpdateModel
    {
        public int ApplicationId { get; set; }
        public string PasswordAlias { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Encrypted { get; set; }
    }
}
