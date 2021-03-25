using Microsoft.AspNet.Identity;
using PMDataManager.Library.DataAccess;
using PMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PMDataManager.Controllers
{
    [Authorize]
    public class PasswordsController : ApiController
    {
        public List<PasswordModel> Get()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            PasswordData data = new PasswordData();

            return data.GetPasswordsByUserId(userId);
        }
    }
}