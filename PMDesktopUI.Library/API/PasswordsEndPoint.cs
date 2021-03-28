using PMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.API
{
    public class PasswordsEndPoint : IPasswordsEndPoint
    {
        private IAPIHelper _apiHelper;

        public PasswordsEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<PasswordModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Passwords"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var result = await response.Content.ReadAsAsync<List<PasswordModel>>();

                return result;
            }
        }

        public async Task UpdatePassword(int id, PasswordUpdateModel passwordUpdateModel)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync($"/api/Passwords/{id}", passwordUpdateModel))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<int> CreateNewPassword(PasswordCreateModel passwordModel)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync($"/api/Passwords", passwordModel))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var result = await response.Content.ReadAsAsync<int>();

                return result;
            }
        }

        public async Task DeletePassword(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync($"/api/Passwords/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
