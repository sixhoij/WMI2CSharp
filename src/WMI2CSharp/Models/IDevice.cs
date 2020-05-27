using System.Threading.Tasks;
using WMI2CSharp.Enums;
using WMI2CSharp.Services;

namespace WMI2CSharp.Models
{
    public interface IDevice
    {
        IDevice WithWMIAccessService(WMIAccessService wmiAccessService);

        IDevice WithInformationCategories(params InformationCategory[] searchInformationCategories);

        IDevice WithInformationTypes(params InformationType[] searchInformationTypes);
        
        Task<Device> InitializeAsync();
    }
}
