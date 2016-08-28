using Mntone.Nico2;
using Mntone.Nico2.Users;

namespace Crux.Extensions
{
    /// <summary>
    /// </summary>
    public static class UserApiExt
    {
        public static string GetIconUrl(this UserApi obj, uint userId)
            => string.Format(NiconicoUrls.UserIconUrl, userId / 10000, userId);
    }
}