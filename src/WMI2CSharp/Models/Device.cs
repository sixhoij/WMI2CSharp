using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WMI2CSharp.Attributes;
using WMI2CSharp.Enums;
using WMI2CSharp.Exceptions;
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

        public List<Query> Queries { get; private set; }

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

        [InformationCategory(InformationCategory.DataFiles)]
        public ICollection<DataFile> DataFiles { get; set; } = new List<DataFile>();

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

        [InformationCategory(InformationCategory.System, InformationCategory.OperatingSystem)]
        public ICollection<MappedDisk> MappedDisks { get; set; } = new List<MappedDisk>();

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

        [InformationCategory(InformationCategory.Software, InformationCategory.Application)]
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

        [InformationCategory(InformationCategory.Software, InformationCategory.Application)]
        public ICollection<Service> Services { get; set; } = new List<Service>();

        [InformationCategory(InformationCategory.System, InformationCategory.OperatingSystem)]
        public SoftwareLicensingService SoftwareLicensingService { get; set; } = new SoftwareLicensingService();

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
            Queries = new List<Query>();
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

        public Device(WMIAccessService wmiAccessService, InformationCategory[] informationCategories = null, params InformationType[] informationTypes) : this()
        {
            _wmiAccessService = wmiAccessService;
            InformationTypes = informationTypes;
            InformationCategories = informationCategories;
        }

        /// <summary>
        /// Device to use the provided WMIAccessService as access
        /// </summary>
        /// <param name="wmiAccessService">Provides access</param>
        /// <returns>Returns this with provided WMIAccessService</returns>
        public IDevice WithWMIAccessService(WMIAccessService wmiAccessService)
        {
            _wmiAccessService = wmiAccessService;
            return this;
        }

        /// <summary>
        /// Provided InformationCategories will be gathered
        /// </summary>
        /// <param name="searchInformationCategories">Search categories</param>
        /// <returns>Returns this with search categories</returns>
        public IDevice WithInformationCategories(params InformationCategory[] searchInformationCategories)
        {
            InformationCategories = searchInformationCategories;
            return this;
        }

        /// <summary>
        /// Provided InformationTypes will be gathered
        /// </summary>
        /// <param name="searchInformationTypes">Search types</param>
        /// <returns>Returns this with search types</returns>
        public IDevice WithInformationTypes(params InformationType[] searchInformationTypes)
        {
            InformationTypes = searchInformationTypes;
            return this;
        }

        /// <summary>
        /// Provided queries will be gathered
        /// </summary>
        /// <param name="queries">List of query object containing InformationType and a string with where statement</param>
        /// <returns>Returns this with queries</returns>
        public IDevice WithQueries(IEnumerable<Query> queries)
        {
            Queries.AddRange(queries);
            return this;
        }

        /// <summary>
        /// Provided wheres will be gathered
        /// </summary>
        /// <param name="informationType">Information type</param>
        /// <param name="where">Where statement</param>
        /// <returns>Returns this with wheres</returns>
        public IDevice Where(InformationType informationType, string where)
        {
            Queries.Add(new Query(informationType, where));
            return this;
        }

        /// <summary>
        /// Initialize information gathering, when method is finished, properties of this Device will be filled with the gathered data.
        /// </summary>
        /// <returns>Returns awaitable Task with this Device</returns>
        public async Task<Device> InitializeAsync()
        {
            EvaluateSearchOptions();
            var cancellationToken = new CancellationToken(false);
            var start = DateTime.Now;
            try
            {
                await GetDeviceInformationAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException operationCanceledException)
            {
                LogEventHandler.TaskIncompleted(operationCanceledException);
            }
            ExecutionTime = DateTime.Now - start;
            return this;
        }

        /// <summary>
        /// Initialize information gathering, when method is finished, properties of this Device will be filled with the gathered data
        /// </summary>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with this Device</returns>
        public async Task<Device> InitializeAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            EvaluateSearchOptions();

            var start = DateTime.Now;
            try
            {
                await GetDeviceInformationAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException operationCanceledException)
            {
                LogEventHandler.TaskIncompleted(operationCanceledException);
            }
            ExecutionTime = DateTime.Now - start;
            return this;
        }

        private void EvaluateSearchOptions()
        {
            if (InformationCategories == null)
            {
                InformationCategories = new InformationCategory[0];
            }

            if (InformationTypes == null)
            {
                InformationTypes = new InformationType[0];
            }

            if (InformationCategories.Contains(InformationCategory.All))
            {
                InformationCategories = new[] { InformationCategory.All };
            }

            if (InformationTypes.Contains(InformationType.All))
            {
                InformationTypes = new[] { InformationType.All };
            }

            if (InformationCategories == null && InformationTypes == null ||
                InformationCategories != null && InformationTypes != null && !InformationTypes.Any() &&
                !InformationCategories.Any())
            {
                InformationCategories = new[]
                {
                    InformationCategory.All
                };
            }
        }

        private async Task GetDeviceInformationAsync(CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var propertyInfo in GetType().GetProperties())
            {
                var propertyType = propertyInfo.PropertyType;
                var isCollection = IsCollection(propertyType);
                propertyType = SetPropertyType(isCollection, propertyType);
                var wmiClassName = GetWMIClassName(propertyType);
                tasks.Add(FetchDataAsync(propertyInfo, isCollection, propertyType, wmiClassName, cancellationToken));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private Task FetchDataAsync(PropertyInfo propertyInfo, bool isCollection, Type propertyType, string wmiClassName, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                if (!string.IsNullOrEmpty(wmiClassName) && ShouldFetchData(propertyInfo))
                {
                    if (isCollection)
                    {
                        await FillCollectionPropertyPropertiesAsync(propertyInfo, propertyType, wmiClassName, cancellationToken).ConfigureAwait(false);
                    }
                    else
                    {
                        await FillPropertyPropertiesAsync(propertyInfo, wmiClassName, cancellationToken).ConfigureAwait(false);
                    }
                }
            }, cancellationToken);
        }

        private string CreateWhereClause(string where)
        {
            var whereClause = where;
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = string.Empty;
            }
            if (!whereClause.Contains("where"))
            {
                whereClause = whereClause.Insert(0, " where ");
            }
            return whereClause;
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
                propertyType = propertyType?.GenericTypeArguments?.First();
            }
            return propertyType;
        }

        private bool IsCollection(Type propertyType)
        {
            return propertyType.GenericTypeArguments.Any();
        }

        private bool ShouldFetchData(MemberInfo memberInfo)
        {
            return (InformationCategorySelected(memberInfo) ||
                   InformationTypeSelected(memberInfo)) &&
                   IsDataFilesSelectedWithoutWhere(memberInfo);
        }

        private bool IsDataFilesSelectedWithoutWhere(MemberInfo memberInfo)
        {
            if (InformationTypes != null || InformationCategories != null)
            {
                var typeName = memberInfo?.Name;
                var informationTypeParsed = Enum.TryParse(typeName, out InformationType informationType);

                if ((InformationTypes == null || !InformationTypes.Contains(InformationType.All) || !InformationTypes.Contains(InformationType.DataFiles)) ||
                    (InformationCategories == null || !InformationCategories.Contains(InformationCategory.DataFiles)) || !InformationCategories.Contains(InformationCategory.All))
                {
                    var shouldFetchDataFiles = informationTypeParsed && informationType != InformationType.DataFiles ||
                                               Queries.Any(x => x.InformationType == InformationType.DataFiles);
                    return shouldFetchDataFiles;
                }
            }

            return false;
        }

        private bool InformationTypeSelected(MemberInfo memberInfo)
        {
            if (InformationTypes != null)
            {
                if (InformationTypes.Contains(InformationType.All))
                {
                    return true;
                }

                var typeName = memberInfo?.Name;
                var parsed = Enum.TryParse(typeName, out InformationType informationType);
                return parsed && InformationTypes.Contains(informationType);
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

        private async Task FillPropertyPropertiesAsync(PropertyInfo propertyInfo, string wmiClassName, CancellationToken cancellationToken)
        {
            if (_wmiAccessService != null)
            {
                var wheres = Queries.Where(x => x.InformationType.ToString() == propertyInfo.Name).Select(x => x.Where);
                if (wheres.Any())
                {
                    var tasks = new List<Task>();
                    foreach (var where in wheres)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            var whereClause = CreateWhereClause(where);
                            await FetchAndMapObjectAsync(propertyInfo, wmiClassName, whereClause, cancellationToken).ConfigureAwait(false);
                        }, cancellationToken));
                    }
                    await Task.WhenAll(tasks).ConfigureAwait(false);
                }
                else
                {
                    await FetchAndMapObjectAsync(propertyInfo, wmiClassName, string.Empty, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        private async Task FetchAndMapObjectAsync(PropertyInfo propertyInfo, string wmiClassName, string whereClause, CancellationToken cancellationToken)
        {
            var managementBaseObject = await (_wmiAccessService?.TryQueryAsync(wmiClassName, whereClause, cancellationToken)).ConfigureAwait(false);
            var obj = await TryMapObjectAsync(managementBaseObject, propertyInfo?.PropertyType).ConfigureAwait(false);
            propertyInfo?.SetValue(this, obj);
        }

        private async Task FillCollectionPropertyPropertiesAsync(PropertyInfo propertyInfo, Type propertyType, string wmiClassName, CancellationToken cancellationToken)
        {
            if (_wmiAccessService != null)
            {
                var wheres = Queries.Where(x => x.InformationType.ToString() == propertyInfo.Name).Select(x => x.Where);
                if (wheres.Any())
                {
                    var tasks = new List<Task>();
                    foreach (var where in wheres)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            var whereClause = CreateWhereClause(where);
                            await FetchAndMapCollectionAsync(propertyInfo, propertyType, wmiClassName, whereClause, cancellationToken).ConfigureAwait(false);
                        }, cancellationToken));
                    }
                    await Task.WhenAll(tasks).ConfigureAwait(false);
                }
                else
                {
                    await FetchAndMapCollectionAsync(propertyInfo, propertyType, wmiClassName, string.Empty, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        private async Task FetchAndMapCollectionAsync(PropertyInfo propertyInfo, Type propertyType, string wmiClassName, string whereClause, CancellationToken cancellationToken)
        {
            var managementBaseObjects = await (_wmiAccessService?.TryQueryCollectionAsync(wmiClassName, whereClause, cancellationToken)).ConfigureAwait(false);
            foreach (var managementBaseObject in managementBaseObjects)
            {
                var obj = await TryMapObjectAsync(managementBaseObject, propertyType).ConfigureAwait(false);
                var collection = propertyInfo?.GetValue(this, null);
                propertyInfo?.PropertyType.GetMethod("Add")?.Invoke(collection, new[] { obj });
            }
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
                                var wmiException = new WMIGeneralException(EndPoint, exception);
                                LogEventHandler.Exception(wmiException);
                            }
                        }
                        catch (Exception exception)
                        {
                            var wmiException = new WMIGeneralException(EndPoint, exception);
                            LogEventHandler.Exception(wmiException);
                        }
                    }));
                }
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return objectInstance;
        }

        private void TrySetValue(object objectInstance, PropertyInfo propertyInfo, PropertyData propertyData)
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
                    var wmiException = new WMIGeneralException(EndPoint, exception);
                    LogEventHandler.Exception(wmiException);
                }
            }
        }

        public override string ToString()
        {
            return EndPoint;
        }
    }
}
