using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IUserData _userData;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IUserData userData)
        {
            _context = context;
            _userManager = userManager;
            _userData = userData;
        }

        [HttpGet]
        public UserModel GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userData.GetUserById(userId);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public void Post([FromBody] UserAddModel userAddModel)
        {

        }

        [HttpPut]
        public void Put([FromBody] UserUpdateModel userAddModel)
        {

        }
    }
}
