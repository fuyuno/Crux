using System.Threading.Tasks;

using Mntone.Nico2;

namespace Crux.Services.Interfaces
{
    internal interface IAccountService
    {
        NiconicoContext CurrentContext { get; }

        Task LoginAsync(string mailAddress, string password);

        Task LogoutAsync();
    }
}