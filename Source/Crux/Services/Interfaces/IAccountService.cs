using System.Threading.Tasks;

using Crux.Models;

using Mntone.Nico2;

namespace Crux.Services.Interfaces
{
    public interface IAccountService
    {
        NiconicoContext CurrentContext { get; }

        User CurrentUser { get; }

        bool IsLoggedIn { get; }

        Task LoginAsync(string mailAddress, string password);

        Task LoginAsync();

        Task LogoutAsync();
    }
}