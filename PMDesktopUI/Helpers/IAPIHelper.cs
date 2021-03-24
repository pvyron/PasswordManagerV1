using PMDesktopUI.Models;
using System.Threading.Tasks;

namespace PMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}