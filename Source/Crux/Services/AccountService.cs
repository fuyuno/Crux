using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Windows.Security.Credentials;

using Crux.Services.Interfaces;

using Mntone.Nico2;

namespace Crux.Services
{
    internal class AccountService : IAccountService
    {
        #region Implementation of IAccountService

        public NiconicoContext CurrentContext { get; private set; }

        public async Task LoginAsync(string mailAddress, string password)
        {
            var loginContext = new NiconicoContext(new NiconicoAuthenticationToken(mailAddress, password))
            {
                AdditionalUserAgent = $"{CruxConstants.Name}/{CruxConstants.Version}"
            };
            var status = await loginContext.SignInAsync();
            if (status != NiconicoSignInStatus.Success)
                return;
            CurrentContext = loginContext;

            try
            {
                var vault = new PasswordVault();
                vault.Add(new PasswordCredential(CruxConstants.Key, mailAddress, password));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task LogoutAsync()
        {
            if (CurrentContext == null)
                return;
            await CurrentContext.SignOutOffAsync();
            CurrentContext.Dispose();
            CurrentContext = null;

            try
            {
                var vault = new PasswordVault();
                vault.RetrieveAll();

                var resources = vault.FindAllByResource(CruxConstants.Key);
                foreach (var credential in resources)
                    vault.Remove(credential);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion
    }
}