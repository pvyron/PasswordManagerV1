using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMDataManager.Library.DataAccess;
using PMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PMDataManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Verified")]
    public class PasswordsController : ControllerBase
    {
        public List<PasswordModel> Get()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData();

            return data.GetPasswordsByUserId(userId);
        }

        public PasswordModel Get(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData();

            return data.GetPasswordsByUserId(userId).Find(p => p.Id == id);
        }

        public int Post([FromBody] PasswordCreateModel passwordCreateModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData();

            return data.CreatePassword(userId, passwordCreateModel);
        }

        public void Put(int id, [FromBody] PasswordUpdateModel passwordUpdateModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData();

            data.UpdatePasswordForUser(id, userId, passwordUpdateModel);
        }

        public void Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData();

            data.DeletePasswordForUser(id, userId);
        }
    }
}
