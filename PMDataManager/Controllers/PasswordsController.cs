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
        private readonly IConfiguration _config;

        public PasswordsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public List<PasswordModel> Get()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData(_config);

            return data.GetPasswordsByUserId(userId);
        }

        [HttpGet]
        [Route("{id}")]
        public PasswordModel Get(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData(_config);

            return data.GetPasswordsByUserId(userId).Find(p => p.Id == id);
        }

        [HttpPost]
        public int Post([FromBody] PasswordCreateModel passwordCreateModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData(_config);

            return data.CreatePassword(userId, passwordCreateModel);
        }

        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] PasswordUpdateModel passwordUpdateModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData(_config);

            data.UpdatePasswordForUser(id, userId, passwordUpdateModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PasswordData data = new PasswordData(_config);

            data.DeletePasswordForUser(id, userId);
        }
    }
}
