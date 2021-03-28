using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public string GetAPI()
        {
            string api = ConfigurationManager.AppSettings["api"];

            return api;
        }

        public string GetDefaultUsername()
        {
            try
            {
                string defaultUsername = ConfigurationManager.AppSettings["defaultUsername"];

                return defaultUsername;
            }
            catch
            {
                // LOG IT
                return "";
            }
        }

        public string GetDefaultPassword()
        {
            try
            {
                string defaultPassword = ConfigurationManager.AppSettings["defaultPassword"];

                return defaultPassword;
            }
            catch
            {
                // LOG IT
                return "";
            }
        }
    }
}
