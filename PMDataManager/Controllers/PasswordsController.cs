using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IPasswordData _passwordData;

        public PasswordsController(IPasswordData passwordData)
        {
            _passwordData = passwordData;
        }

        [HttpGet]
        public List<PasswordModel> Get()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _passwordData.GetPasswordsByUserId(userId);
        }

        [HttpGet]
        [Route("{id}")]
        public PasswordModel Get(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _passwordData.GetPasswordsByUserId(userId).Find(p => p.Id == id);
        }

        [HttpPost]
        public int Post([FromBody] PasswordCreateModel passwordCreateModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _passwordData.CreatePassword(userId, passwordCreateModel);
        }

        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] PasswordUpdateModel passwordUpdateModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _passwordData.UpdatePasswordForUser(id, userId, passwordUpdateModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _passwordData.DeletePasswordForUser(id, userId);
        }
    }
}
