using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PMDataManager.Data;
using PMDataManager.Library.DataAccess;
using PMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PMDataManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserData _userData;
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration config, IUserData userData, ILogger<AdminController> logger)
        {
            _config = config;
            _userData = userData;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Users")]
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
        [Route("Roles")]
        [Authorize(Roles = "Administrator")]
        public Dictionary<string, string> GetAllRoles()
        {
            var roles = _context.Roles.ToDictionary(r => r.Id, r => r.Name);

            return roles;
        }

        [HttpPost]
        [Route("Users/Roles/Add")]
        [Authorize(Roles = "Administrator")]
        public async Task AddRole([FromBody] UserRolePairModel pairing)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(pairing.UserId);

            _logger.LogInformation("Admin {Admin} added user {User} to role {Role}", 
                loggedInUserId, user.Id, pairing.RoleName);

            await _userManager.AddToRoleAsync(user, pairing.RoleName);
        }

        [HttpPost]
        [Route("Users/Roles/Remove")]
        [Authorize(Roles = "Administrator")]
        public async Task RemoveRole([FromBody] UserRolePairModel pairing)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(pairing.UserId);

            _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}",
                loggedInUserId, user.Id, pairing.RoleName);

            await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);
        }
    }
}
