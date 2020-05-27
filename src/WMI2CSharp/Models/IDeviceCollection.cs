using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WMI2CSharp.Enums;
using WMI2CSharp.Providers;

namespace WMI2CSharp.Models
{
    public interface IDeviceCollection
    {
        IDeviceCollection WithCredentials(ICredentialsProvider credentialsProvider);

        IDeviceCollection WithInformationCategories(params InformationCategory[] informationCategories);

        IDeviceCollection WithInformationTypes(params InformationType[] informationTypes);

        Task<ICollection<Device>> GetDevicesAsync(IEnumerable<string> hostNames);

        Task<ICollection<Device>> GetDevicesAsync(IEnumerable<string> hostNames, CancellationToken cancellationToken);
    }
}
