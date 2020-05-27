using System.Threading.Tasks;
using WMI2CSharp.Models;

namespace WMI2CSharp.Providers
{
    public class CredentialsProvider : ICredentialsProvider
    {
        private readonly string _user;
        private readonly string _pass;
        private readonly string _domain;

        public CredentialsProvider(string user, string pass, string domain = null)
        {
            _user = user;
            _pass = pass;
            _domain = domain;
        }

        public async Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName)
        {
            return await Task.Run(() => new WMIConnectionOption(hostName, _domain, _user, _pass)).ConfigureAwait(false);
        }

        public async Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName, string domain)
        {
            return await Task.Run(() => new WMIConnectionOption(hostName, domain, _user, _pass)).ConfigureAwait(false);
        }
    }
}
