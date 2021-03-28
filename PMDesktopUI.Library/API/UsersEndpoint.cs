using PMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.API
{
    public class UsersEndpoint : IUsersEndpoint
    {
        private IAPIHelper _apiHelper;

        public UsersEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<UserModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/admin/Users"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var result = await response.Content.ReadAsAsync<List<UserModel>>();

                return result;
            }
        }

        public async Task<Dictionary<string, string>> GetAllRoles()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/admin/Users/Roles"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();

                return result;
            }
        }

        public async Task AddUserToRole(string userId, string roleName)
        {
            var data = new { userId, roleName };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/admin/Users/Roles/Add", data))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task DeleteUserFromRole(string userId, string roleName)
        {
            var data = new { userId, roleName };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/admin/Users/Roles/Remove", data))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
