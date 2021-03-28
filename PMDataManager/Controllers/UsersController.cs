using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PMDataManager.Data;
using PMDataManager.Library.DataAccess;
using PMDataManager.Library.Models;
using PMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PMDataManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public UserModel GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData();

            return data.GetUserById(userId);
        }

        [HttpGet]
        [Route("api/admin/Users")]
        [Authorize(Roles = "Administrator")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> applicationUserModels = new List<ApplicationUserModel>();

            var users = _context.Users.ToList();

            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            foreach (var user in users)
            {
                ApplicationUserModel applicationUserModel = new ApplicationUserModel()
                {
                    Id = user.Id,
                    Email = user.Email
                };

                applicationUserModel.Roles = userRoles
                    .Where(ur => ur.UserId == applicationUserModel.Id)
                    .ToDictionary(ur => ur.RoleId, ur => ur.Name);

                applicationUserModels.Add(applicationUserModel);
            }

            return applicationUserModels;
        }

        [HttpGet]
        [Route("api/admin/Roles")]
        [Authorize(Roles = "Administrator")]
        public Dictionary<string, string> GetAllRoles()
        {
            var roles = _context.Roles.ToDictionary(r => r.Id, r => r.Name);

            return roles;
        }

        [HttpPost]
        [Route("api/admin/Users/Roles/Add")]
        [Authorize(Roles = "Administrator")]
        public async Task AddRole([FromBody] UserRolePairModel pairing)
        {
            var user = await _userManager.FindByIdAsync(pairing.UserId);

            await _userManager.AddToRoleAsync(user, pairing.RoleName);
        }

        [HttpPost]
        [Route("api/admin/Users/Roles/Remove")]
        [Authorize(Roles = "Administrator")]
        public async Task RemoveRole([FromBody] UserRolePairModel pairing)
        {
            var user = await _userManager.FindByIdAsync(pairing.UserId);

            await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);
        }

        [Authorize(Roles = "Administrator")]
        public void Post([FromBody] UserAddModel userAddModel)
        {

        }

        public void Put([FromBody] UserUpdateModel userAddModel)
        {

        }
    }
}
