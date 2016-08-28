using Mntone.Nico2;
using Mntone.Nico2.Users.Info;

namespace Crux.Models
{
    public class User
    {
        private readonly InfoResponse _infoResponse;

        public string Name => _infoResponse.Name;
        public uint Id => _infoResponse.Id;
        public bool IsPremium => _infoResponse.IsPremium;
        public string IconUrl => string.Format(NiconicoUrls.UserIconUrl, Id / 10000, Id);

        public User(InfoResponse infoResponse)
        {
            _infoResponse = infoResponse;
        }
    }
}