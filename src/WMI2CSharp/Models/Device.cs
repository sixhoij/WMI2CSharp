using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading.Tasks;
using WMI2CSharp.Attributes;
using WMI2CSharp.Enums;
using WMI2CSharp.Log;
using WMI2CSharp.Models.DeviceModels;
using WMI2CSharp.Services;
using Environment = WMI2CSharp.Models.DeviceModels.Environment;
using OperatingSystem = WMI2CSharp.Models.DeviceModels.OperatingSystem;
using TimeZone = WMI2CSharp.Models.DeviceModels.TimeZone;

namespace WMI2CSharp.Models
{
    /// <summary>
    /// Represents a Windows device with information gathered with WMI (Windows Management Instrumentation)
    /// </summary>
    public class Device : IDevice
    {
        private WMIAccessService _wmiAccessService;

        public InformationType[] InformationTypes { get; private set; }

        public InformationCategory[] InformationCategories { get; private set; }

        public string EndPoint => _wmiAccessService.EndPoint;

        public TimeSpan ExecutionTime { get; private set; }

        [InformationCategory(InformationCategory.InternalHardware)]
        public BaseBoard BaseBoard { get; set; } = new BaseBoard();

        [InformationCategory(InformationCategory.InternalHardware, InformationCategory.OperatingSystem)]
        public BIOS BIOS { get; set; } = new BIOS();

        [InformationCategory(InformationCategory.InternalHardware, InformationCategory.OperatingSystem)]
        public ComputerSystem ComputerSystem { get; set; } = new ComputerSystem();

        [InformationCategory(InformationCategory.User, InformationCategory.System)]
        public ICollection<Desktop> Desktops { get; set; } = new List<Desktop>();

        [InformationCategory(InformationCategory.InternalHardware)]
        public ICollection<Disk> Disks { get; set; } = new List<Disk>();

        [InformationCategory(InformationCategory.InternalHardware)]
        public ICollection<DiskDrive> DiskDrives { get; set; } = new List<DiskDrive>();

        [InformationCategory(InformationCategory.InternalHardware)]
        public ICollection<DiskPartition> DiskPartitions { get; set; } = new List<DiskPartition>();

        [InformationCategory(InformationCategory.System)]
        public ICollection<Environment> Environments { get; set; } = new List<Environment>();

        [InformationCategory(InformationCategory.System, InformationCategory.OperatingSystem)]
        public LocalTime LocalTime { get; set; } = new LocalTime();

        [InformationCategory(InformationCategory.InternalHardware, InformationCategory.Network)]
        public ICollection<NetworkAdapter> NetworkAdapters { get; set; } = new List<NetworkAdapter>();

        [InformationCategory(InformationCategory.Configuration)]
        public ICollection<NetworkAdapterConfiguration> NetworkAdapterConfigurations { get; set; } = new List<NetworkAdapterConfiguration>();

        [InformationCategory(InformationCategory.System, InformationCategory.OperatingSystem)]
        public OperatingSystem OperatingSystem { get; set; } = new OperatingSystem();

        [InformationCategory(InformationCategory.InternalHardware)]
        public ICollection<PhysicalMemory> PhysicalMemories { get; set; } = new List<PhysicalMemory>();

        [InformationCategory(InformationCategory.PNP)]
        public ICollection<PNPEntity> PNPEntities { get; set; } = new List<PNPEntity>();

        [InformationCategory(InformationCategory.PNP)]
        public ICollection<PNPSignedDriver> PNPSignedDrivers { get; set; } = new List<PNPSignedDriver>();

        [InformationCategory(InformationCategory.ExternalHardware)]
        public ICollection<Printer> Printers { get; set; } = new List<Printer>();

        [InformationCategory(InformationCategory.Configuration)]
        public ICollection<PrinterConfiguration> PrinterConfigurations { get; set; } = new List<PrinterConfiguration>();

        [InformationCategory(InformationCategory.Software, InformationCategory.Application, InformationCategory.System)]
        public ICollection<Process> Processes { get; set; } = new List<Process>();

        [InformationCategory(InformationCategory.InternalHardware)]
        public Processor Processor { get; set; } = new Processor();

        [InformationCategory(InformationCategory.Software, InformationCategory.Application)]
        public ICollection<Product> Products { get; set; } = new List<Product>();

        [InformationCategory(InformationCategory.Software, InformationCategory.System)]
        public ICollection<QuickFixEngineering> QuickFixEngineerings { get; set; } = new List<QuickFixEngineering>();

        [InformationCategory(InformationCategory.ExternalHardware)]
        public ICollection<SerialPort> SerialPorts { get; set; } = new List<SerialPort>();

        [InformationCategory(InformationCategory.Configuration)]
        public ICollection<SerialPortConfiguration> SerialPortConfigurations { get; set; } = new List<SerialPortConfiguration>();

        [InformationCategory(InformationCategory.Software, InformationCategory.Application, InformationCategory.System)]
        public ICollection<Service> Services { get; set; } = new List<Service>();

        [InformationCategory(InformationCategory.System, InformationCategory.OperatingSystem)]
        public TimeZone TimeZone { get; set; } = new TimeZone();

        [InformationCategory(InformationCategory.InternalHardware)]
        public ICollection<VideoController> VideoControllers { get; set; } = new List<VideoController>();

        [InformationCategory(InformationCategory.InternalHardware)]
        public ICollection<Volume> Volumes { get; set; } = new List<Volume>();

        [InformationCategory(InformationCategory.System, InformationCategory.OperatingSystem)]
        public WindowsScore WindowsScore { get; set; } = new WindowsScore();

        public Device()
        {
        }

        public Device(params InformationCategory[] informationCategories) : this(new WMIAccessService(), informationCategories)
        {
        }

        public Device(params InformationType[] informationTypes) : this(new WMIAccessService(), informationTypes)
        {
        }

        public Device(WMIAccessService wmiAccessService, params InformationType[] informationTypes) : this(wmiAccessService, null, informationTypes)
        {
        }

        public Device(WMIAccessService wmiAccessService, InformationCategory[] informationCategories = null, params InformationType[] informationTypes)
        {
            _wmiAccessService = wmiAccessService;
            InformationTypes = informationTypes;
            InformationCategories = informationCategories;
        }

        /// <summary>
        /// Device to use the provided WMIAccessService as access.
        /// </summary>
        /// <param name="wmiAccessService">Provides access.</param>
        /// <returns>Returns this with provided WMIAccessService.</returns>
        public IDevice WithWMIAccessService(WMIAccessService wmiAccessService)
        {
            _wmiAccessService = wmiAccessService;
            return this;
        }


        /// <summary>
        /// Only provided InformationCategories will be gathered.
        /// </summary>
        /// <param name="searchInformationCategories">Search categories.</param>
        /// <returns>Returns this with search categories.</returns>
        public IDevice WithInformationCategories(params InformationCategory[] searchInformationCategories)
        {
            InformationCategories = searchInformationCategories;
            return this;
        }

        /// <summary>
        /// Only provided InformationTypes will be gathered.
        /// </summary>
        /// <param name="searchInformationTypes">Search types.</param>
        /// <returns>Returns this with search types.</returns>
        public IDevice WithInformationTypes(params InformationType[] searchInformationTypes)
        {
            InformationTypes = searchInformationTypes;
            return this;
        }

        /// <summary>
        /// Initialize information gathering, when method is finished, properties of this Device will be filled with the gathered data.
        /// </summary>
        /// <returns>Returns awaitable Task</returns>
        public async Task<Device> InitializeAsync()
        {
            var start = DateTime.Now;
            await GetDeviceInformationAsync().ConfigureAwait(false);
            ExecutionTime = DateTime.Now - start;
            return this;
        }

        private async Task GetDeviceInformationAsync()
        {
            var tasks = new List<Task>();

            foreach (var propertyInfo in GetType().GetProperties())
            {
                var propertyType = propertyInfo.PropertyType;
                var isCollection = IsCollection(propertyType);
                propertyType = SetPropertyType(isCollection, propertyType);
                var wmiClassName = GetWMIClassName(propertyType);
                tasks.Add(FetchDataAsync(propertyInfo, isCollection, propertyType, wmiClassName));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private Task FetchDataAsync(PropertyInfo propertyInfo, bool isCollection, Type propertyType, string wmiClassName)
        {
            return Task.Run(async () =>
            {
                if (!string.IsNullOrEmpty(wmiClassName) && ShouldFetchData(propertyInfo))
                {
                    if (isCollection)
                    {
                        await FillCollectionPropertyPropertiesAsync(propertyInfo, propertyType, wmiClassName).ConfigureAwait(false);
                    }
                    else
                    {
                        await FillPropertyPropertiesAsync(propertyInfo, wmiClassName).ConfigureAwait(false);
                    }
                }
            });
        }

        private string GetWMIClassName(MemberInfo propertyType)
        {
            var wmiClassAttribute = propertyType?.CustomAttributes?.FirstOrDefault(x => x.AttributeType == typeof(WMIClassAttribute));
            var wmiClassName = wmiClassAttribute?.ConstructorArguments.FirstOrDefault().Value?.ToString();
            return wmiClassName;
        }

        private Type SetPropertyType(bool isCollection, Type propertyType)
        {
            if (isCollection)
            {
                propertyType = propertyType?.GenericTypeArguments?.ToList().First();
            }
            return propertyType;
        }

        private bool IsCollection(Type propertyType)
        {
            return propertyType.GenericTypeArguments.ToList().Any();
        }

        private bool ShouldFetchData(MemberInfo memberInfo)
        {
            return InformationCategorySelected(memberInfo) || InformationTypeSelected(memberInfo);
        }

        private bool InformationTypeSelected(MemberInfo memberInfo)
        {
            if (InformationTypes != null)
            {
                var typeName = memberInfo?.Name;
                var parsed = Enum.TryParse(typeName, out InformationType informationType);
                if (parsed && InformationTypes.Contains(informationType))
                {
                    return true;
                }
            }

            return false;
        }

        private bool InformationCategorySelected(MemberInfo memberInfo)
        {
            if (InformationCategories != null)
            {
                if (InformationCategories.Contains(InformationCategory.All))
                {
                    return true;
                }

                var informationCategories = memberInfo?.GetCustomAttributes(typeof(InformationCategoryAttribute), true)
                                                       .Cast<InformationCategoryAttribute>()
                                                       .FirstOrDefault()?
                                                       .InformationCategories;

                if (informationCategories != null)
                {
                    foreach (var informationCategory in informationCategories)
                    {
                        if (InformationCategories.Contains(informationCategory))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private Task FillPropertyPropertiesAsync(PropertyInfo modelPropertyInfo, string wmiClassName)
        {
            return Task.Run(async () =>
            {
                if (_wmiAccessService != null)
                {
                    var managementBaseObject = await (_wmiAccessService?.TryQueryAsync(wmiClassName)).ConfigureAwait(false);
                    var obj = await TryMapObjectAsync(managementBaseObject, modelPropertyInfo?.PropertyType).ConfigureAwait(false);
                    modelPropertyInfo?.SetValue(this, obj);
                }
            });
        }

        private Task FillCollectionPropertyPropertiesAsync(PropertyInfo modelPropertyInfo, Type propertyType, string wmiClassName)
        {
            return Task.Run(async () =>
            {
                var tasks = new List<Task>();
                if (_wmiAccessService != null)
                {
                    var managementBaseObjects = await (_wmiAccessService?.TryQueryCollectionAsync(wmiClassName)).ConfigureAwait(false);
                    foreach (var managementBaseObject in managementBaseObjects)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            var obj = await TryMapObjectAsync(managementBaseObject, propertyType).ConfigureAwait(false);
                            var collection = modelPropertyInfo?.GetValue(this, null);
                            modelPropertyInfo?.PropertyType.GetMethod("Add")?.Invoke(collection, new[] { obj });
                        }));
                    }
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
            });
        }

        private async Task<object> TryMapObjectAsync(ManagementBaseObject managementBaseObject, Type propertyType)
        {
            var tasks = new List<Task>();
            var objectInstance = Activator.CreateInstance(propertyType);
            var objectInstanceName = objectInstance?.GetType().Name;

            if (objectInstance != null)
            {
                foreach (var propertyInfo in objectInstance.GetType().GetProperties())
                {
                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            var propertyData = managementBaseObject?.Properties[propertyInfo.Name];
                            if (!(propertyData is null))
                            {
                                TrySetValue(objectInstance, propertyInfo, propertyData);
                                LogEventHandler.Information($"{EndPoint}: {objectInstanceName}: {propertyInfo.Name}: {propertyData.Value}");
                            }
                        }
                        catch (ManagementException exception)
                        {
                            if (exception.ErrorCode != ManagementStatus.NotFound)
                            {
                                LogEventHandler.Exception(exception);
                            }
                        }
                        catch (Exception exception)
                        {
                            LogEventHandler.Exception(exception);
                        }
                    }));
                }
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return objectInstance;
        }

        private static void TrySetValue(object objectInstance, PropertyInfo propertyInfo, PropertyData propertyData)
        {
            try
            {
                propertyInfo.SetValue(objectInstance, propertyData?.Value);
            }
            catch (Exception)
            {
                try
                {
                    if (propertyInfo != null)
                    {
                        if (propertyInfo.ReflectedType != null && propertyData?.Type != null)
                        {
                            LogEventHandler.Warning($"{objectInstance.GetType().FullName}.{propertyData.Name} : should be type of {propertyData.Type}. Failed with value: {propertyData.Value}");
                        }
                        propertyInfo.SetValue(objectInstance, propertyData?.Value?.ToString());
                    }
                }
                catch (Exception exception)
                {
                    LogEventHandler.Exception(exception);
                }
            }
        }

        public override string ToString()
        {
            return EndPoint;
        }
    }
}
