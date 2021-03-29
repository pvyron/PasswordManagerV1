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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ApplicationsController(IConfiguration config)
        {
            _config = config;
        }

        public List<ApplicationModel> Get()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationData data = new ApplicationData(_config);

            return data.GetApplicationsByUserId(userId);
        }

        public ApplicationModel Get(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationData data = new ApplicationData(_config);

            return data.GetApplicationsByUserId(userId).Find(a => a.Id == id);
        }

        //public void Post([FromBody] ApplicationCreateModel applicationCreateModel)
        //{

        //}

        //public void Put(int id, [FromBody] ApplicationUpdateModel applicationUpdateModel)
        //{

        //}

        public void Delete(int id)
        {

        }
    }
}
