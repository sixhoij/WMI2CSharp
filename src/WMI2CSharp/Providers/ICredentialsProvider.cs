using System.Threading.Tasks;
using WMI2CSharp.Models;

namespace WMI2CSharp.Providers
{
    public interface ICredentialsProvider
    {
        Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName);

        Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName, string domain);
    }
}
