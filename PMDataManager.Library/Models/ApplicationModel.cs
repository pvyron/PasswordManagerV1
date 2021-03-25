using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataManager.Library.Models
{
    public class ApplicationModel
    {
		public int Id { get; set; }
		public string UserId { get; set; }
		public string ApplicationAlias { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public bool DefaultEncryption { get; set; }
	}
}
