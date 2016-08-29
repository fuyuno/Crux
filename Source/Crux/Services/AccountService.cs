using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Windows.Security.Credentials;

using Crux.Models;
using Crux.Services.Interfaces;

using Mntone.Nico2;

namespace Crux.Services
{
    internal class AccountService : IAccountService
    {
        #region Implementation of IAccountService

        public NiconicoContext CurrentContext { get; private set; }
        public User CurrentUser { get; private set; }
        public bool IsLoggedIn => (CurrentContext != null) && (CurrentUser != null);

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
            CurrentUser = new User(await CurrentContext.User.GetInfoAsync());

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

        public async Task LoginAsync()
        {
            try
            {
                var vault = new PasswordVault();
                vault.RetrieveAll();

                var resources = vault.FindAllByResource(CruxConstants.Key);
                var credential = resources.FirstOrDefault();
                if (credential == null)
                    return;

                credential.RetrievePassword();
                var loginContext = new NiconicoContext(new NiconicoAuthenticationToken(credential.UserName, credential.Password))
                {
                    AdditionalUserAgent = $"{CruxConstants.Name}/{CruxConstants.Version}"
                };
                var status = await loginContext.SignInAsync();
                if (status != NiconicoSignInStatus.Success)
                    return;
                CurrentContext = loginContext;
                CurrentUser = new User(await CurrentContext.User.GetInfoAsync());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task LogoutAsync(bool isDelCredentials = true)
        {
            if (CurrentContext == null)
                return;
            var status = await CurrentContext.SignOutOffAsync();
            if (status != NiconicoSignInStatus.Failed)
                return;
            CurrentContext.Dispose();
            CurrentContext = null;
            CurrentUser = null;
            if (!isDelCredentials)
                return;
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