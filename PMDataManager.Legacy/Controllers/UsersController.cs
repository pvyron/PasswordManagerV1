using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PMDataManager.Library.DataAccess;
using PMDataManager.Library.Models;
using PMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PMDataManager.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userId);
        }

        [HttpGet]
        [Route("api/admin/Users")]
        [Authorize(Roles = "Administrator")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> applicationUserModels = new List<ApplicationUserModel>();

            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();

                foreach (var user in users)
                {
                    ApplicationUserModel applicationUserModel = new ApplicationUserModel()
                    {
                        Id = user.Id,
                        Email = user.Email
                    };

                    foreach (var userRole in user.Roles)
                    {
                        applicationUserModel.Roles.Add(userRole.RoleId, roles.Where(r => r.Id == userRole.RoleId).First().Name);
                    }

                    applicationUserModels.Add(applicationUserModel);
                }
            }

            return applicationUserModels;
        }

        [HttpGet]
        [Route("api/admin/Roles")]
        [Authorize(Roles = "Administrator")]
        public Dictionary<string, string> GetAllRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                var roles = context.Roles.ToDictionary(r => r.Id, r => r.Name);

                return roles;
            }
        }

        [HttpPost]
        [Route("api/admin/Users/Roles/Add")]
        [Authorize(Roles = "Administrator")]
        public void AddRole([FromBody] UserRolePairModel pairing)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.AddToRole(pairing.UserId, pairing.RoleName);
            }
        }

        [HttpPost]
        [Route("api/admin/Users/Roles/Remove")]
        [Authorize(Roles = "Administrator")]
        public void RemoveRole([FromBody] UserRolePairModel pairing)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.RemoveFromRole(pairing.UserId, pairing.RoleName);
            }
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
