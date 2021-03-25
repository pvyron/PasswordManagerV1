using Microsoft.AspNet.Identity;
using PMDataManager.Library.DataAccess;
using PMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace PMDataManager.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userId);
        }
    }
}
