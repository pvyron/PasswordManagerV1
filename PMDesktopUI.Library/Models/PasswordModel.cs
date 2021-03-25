using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.Models
{
    public class PasswordModel
    {
		public int Id { get; set; }
		public string UserId { get; set; }
		public int ApplicationId { get; set; }
		public string PasswordAlias { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public bool Encrypted { get; set; }
    }
}
