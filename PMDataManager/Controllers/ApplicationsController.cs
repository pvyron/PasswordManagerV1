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
        private readonly IApplicationData _applicationData;

        public ApplicationsController(IConfiguration config, IApplicationData applicationData)
        {
            _config = config;
            _applicationData = applicationData;
        }

        [HttpGet]
        public List<ApplicationModel> Get()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _applicationData.GetApplicationsByUserId(userId);
        }

        [HttpGet]
        [Route("{id}")]
        public ApplicationModel Get(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _applicationData.GetApplicationsByUserId(userId).Find(a => a.Id == id);
        }

        //public void Post([FromBody] ApplicationCreateModel applicationCreateModel)
        //{

        //}

        //public void Put(int id, [FromBody] ApplicationUpdateModel applicationUpdateModel)
        //{

        //}

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {

        }
    }
}
