using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using PMDesktopUI.Library.Models;
using PMDesktopUI.Library.Helpers;

namespace PMDesktopUI.Library.API
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient _apiClient { get; set; }
        private ILoggedInUserModel _loggedInUser;
        private IConfigHelper _configHelper;

        public APIHelper(ILoggedInUserModel loggedInUserModel, IConfigHelper configHelper)
        {
            _configHelper = configHelper;
            
            InitializeClient();

            _loggedInUser = loggedInUserModel;
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }

        private void InitializeClient()
        {
            string api = _configHelper.GetAPI();

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                return result;
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/Users"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var result = await response.Content.ReadAsAsync<LoggedInUserModel>();

                _loggedInUser.Id = result.Id;
                _loggedInUser.LastName = result.LastName;
                _loggedInUser.FirstName = result.FirstName;
                _loggedInUser.UpdateDate = result.UpdateDate;
                _loggedInUser.CreateDate = result.CreateDate;
                _loggedInUser.EmailAddress = result.EmailAddress;

                _loggedInUser.Token = token;
            }
        }
    }
}
