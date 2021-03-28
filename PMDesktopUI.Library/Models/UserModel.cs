using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();

        public string RolesList
        {
            get
            {
                return string.Join(", ", Roles.Select(r => r.Value));
            }
        }
    }
}
