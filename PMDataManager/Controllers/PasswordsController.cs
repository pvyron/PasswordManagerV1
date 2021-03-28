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
    [Authorize(Roles = "Verified")]
    public class PasswordsController : ApiController
    {
        public List<PasswordModel> Get()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            PasswordData data = new PasswordData();

            return data.GetPasswordsByUserId(userId);
        }

        public PasswordModel Get(int id)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            PasswordData data = new PasswordData();

            return data.GetPasswordsByUserId(userId).Find(p => p.Id == id);
        }

        public int Post([FromBody] PasswordCreateModel passwordCreateModel)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            PasswordData data = new PasswordData();

            return data.CreatePassword(userId, passwordCreateModel);
        }

        public void Put(int id, [FromBody] PasswordUpdateModel passwordUpdateModel)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            PasswordData data = new PasswordData();

            data.UpdatePasswordForUser(id, userId, passwordUpdateModel);
        }

        public void Delete(int id)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            PasswordData data = new PasswordData();

            data.DeletePasswordForUser(id, userId);
        }
    }
}