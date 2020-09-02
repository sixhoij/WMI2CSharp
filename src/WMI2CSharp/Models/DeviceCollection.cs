using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMI2CSharp.Enums;
using WMI2CSharp.Exceptions;
using WMI2CSharp.Log;
using WMI2CSharp.Providers;
using WMI2CSharp.Services;

namespace WMI2CSharp.Models
{
    /// <summary>
    /// Represents a collection of the type Device. Implements simplified functionality to gather multiple Devices at once
    /// </summary>
    public class DeviceCollection : IDeviceCollection
    {
        private AccessProvider _accessProvider;

        public List<Device> Devices { get; private set; }

        public List<Query> Queries { get; private set; }

        public DateTime Start { get; private set; }

        public int Properties { get; private set; }

        public int Exceptions { get; private set; }

        public int Connections { get; private set; }

        public int Warnings { get; private set; }

        public int TasksIncompleted { get; private set; }

        public InformationType[] InformationTypes { get; private set; }

        public InformationCategory[] InformationCategories { get; private set; }

        public DeviceCollection(ICredentialsProvider credentialsProvider, InformationCategory[] informationCategories = null, params InformationType[] informationTypes) : this()
        {
            _accessProvider = new AccessProvider(credentialsProvider);
            InformationCategories = informationCategories;
            InformationTypes = informationTypes;
        }

        public DeviceCollection(InformationCategory[] informationCategories = null, params InformationType[] informationTypes) : this()
        {
            InformationCategories = informationCategories;
            InformationTypes = informationTypes;
        }

        public DeviceCollection()
        {
            Setup(true);
            LogEventHandler.OnInformationMessage += (sender, eventArgs) =>
            {
                Properties++;
            };
            LogEventHandler.OnConnectionMessage += (sender, eventArgs) =>
            {
                Connections++;
            };
            LogEventHandler.OnExceptionMessage += (sender, eventArgs) =>
            {
                Exceptions++;
            };
            LogEventHandler.OnWarningMessage += (sender, eventArgs) =>
            {
                Warnings++;
            };
            LogEventHandler.OnTaskIncompleted += (sender, eventArgs) =>
            {
                TasksIncompleted++;
            };
        }

        private void Setup(bool initialize)
        {
            if (initialize)
            {
                Devices = new List<Device>();
                Queries = new List<Query>();
                Properties = 0;
                Exceptions = 0;
                Connections = 0;
                Warnings = 0;
                TasksIncompleted = 0;
                Start = DateTime.Now;
            }
        }

        private bool HasExecuted()
        {
            return Devices != null && Devices.Any();
        }

        /// <summary>
        /// Get a summary of the performance of the Device gathering
        /// </summary>
        /// <returns>Performance summary</returns>
        public string Summarize()
        {
            if (Devices != null && Devices.Any())
            {
                var summaryBuilder = new StringBuilder();
                var executionTime = ExecutionTime;
                var combinedTotalRunTime = CombinedTotalRunTime;
                var avgTime = AverageTimePerDevice.TotalSeconds;

                var fastestTime = TimeSpan.Zero;
                var slowestTime = TimeSpan.Zero;
                Device fastestDevice = null;
                Device slowestDevice = null;
                var belowAvg = 0;
                var aboveAvg = 0;

                foreach (var device in Devices.OrderBy(x => x.ExecutionTime))
                {
                    if (device.ExecutionTime < fastestTime || fastestTime == TimeSpan.Zero)
                    {
                        fastestTime = device.ExecutionTime;
                        fastestDevice = device;
                    }
                    if (device.ExecutionTime > slowestTime || fastestTime == TimeSpan.Zero)
                    {
                        slowestTime = device.ExecutionTime;
                        slowestDevice = device;
                    }
                    if (device.ExecutionTime.TotalSeconds > avgTime)
                    {
                        summaryBuilder.AppendLine($"Execution Time : {device.ExecutionTime} => {device.EndPoint}");
                        aboveAvg++;
                    }
                    if (device.ExecutionTime.TotalSeconds < avgTime)
                    {
                        belowAvg++;
                    }
                }

                summaryBuilder.AppendLine($"Fetched {Devices.Count} devices!");
                summaryBuilder.AppendLine($"Fetched {Properties} properties!");
                summaryBuilder.AppendLine($"Catched {Exceptions} exceptions!");
                summaryBuilder.AppendLine($"Catched {Warnings} warnings!");
                summaryBuilder.AppendLine($"Resolved {Connections} connections!");
                summaryBuilder.AppendLine($"Tasks incompleted: {TasksIncompleted}");
                summaryBuilder.AppendLine($"Execution Time : {executionTime}");
                summaryBuilder.AppendLine($"Combined Total Run Time : {combinedTotalRunTime}");
                summaryBuilder.AppendLine($"Time Saved by running devices async: {combinedTotalRunTime - executionTime}");
                summaryBuilder.AppendLine($"Execution Time: Seconds per device : {executionTime.TotalSeconds / Devices.Count}");
                summaryBuilder.AppendLine($"Execution Time: Seconds per property : {executionTime.TotalSeconds / Properties}");
                summaryBuilder.AppendLine($"Execution Time: Properties per second : {Properties / executionTime.TotalSeconds}");
                summaryBuilder.AppendLine($"Average Time: Seconds per device : {avgTime / Devices.Count}");
                summaryBuilder.AppendLine($"Average Time: Seconds per property : {avgTime / Properties}");
                summaryBuilder.AppendLine($"Average Time: Properties per second : {Properties / avgTime}");
                summaryBuilder.AppendLine($"Fastest Execution Time : {fastestDevice?.ExecutionTime} => {fastestDevice?.EndPoint}");
                summaryBuilder.AppendLine($"Slowest Execution Time : {slowestDevice?.ExecutionTime} => {slowestDevice?.EndPoint}");
                summaryBuilder.AppendLine($"AvgTime : {TimeSpan.FromSeconds(avgTime)}");
                summaryBuilder.AppendLine($"Difference : {slowestDevice?.ExecutionTime - fastestDevice?.ExecutionTime}");
                if (slowestDevice != null && fastestDevice != null)
                {
                    var calculatedAverageTime = TimeSpan.FromSeconds((slowestDevice.ExecutionTime.TotalSeconds + fastestDevice.ExecutionTime.TotalSeconds) / 2);
                    summaryBuilder.AppendLine($"Calculated AvgTime : {calculatedAverageTime}");
                }
                summaryBuilder.AppendLine($"BelowAvg Count : {belowAvg}");
                summaryBuilder.AppendLine($"AboveAvg Count : {aboveAvg}");
                return summaryBuilder.ToString();
            }
            return string.Empty;
        }

        public TimeSpan ExecutionTime => DateTime.Now - Start;

        public TimeSpan CombinedTotalRunTime
        {
            get
            {
                var combinedTotalRunTime = TimeSpan.Zero;
                Devices.ToList().ForEach(device => combinedTotalRunTime += device.ExecutionTime);
                return combinedTotalRunTime;
            }
        }

        public TimeSpan AverageTimePerDevice => new TimeSpan(0, 0, Convert.ToInt32(CombinedTotalRunTime.TotalSeconds / Devices.Count));

        /// <summary>
        /// DeviceCollection to use the provided CredentialsProvider to get access
        /// </summary>
        /// <param name="credentialsProvider">Provides credential</param>
        /// <returns>Returns this with provided CredentialsProvider</returns>
        public IDeviceCollection WithCredentials(ICredentialsProvider credentialsProvider)
        {
            Setup(HasExecuted());
            _accessProvider = new AccessProvider(credentialsProvider);
            return this;
        }

        /// <summary>
        /// Only provided InformationCategories will be gathered
        /// </summary>
        /// <param name="informationCategories">Search categories</param>
        /// <returns>Returns this with search categories</returns>
        public IDeviceCollection WithInformationCategories(params InformationCategory[] informationCategories)
        {
            Setup(HasExecuted());
            InformationCategories = informationCategories;
            return this;
        }

        /// <summary>
        /// Only provided InformationTypes will be gathered
        /// </summary>
        /// <param name="informationTypes">Search types</param>
        /// <returns>Returns this with search types</returns>
        public IDeviceCollection WithInformationTypes(params InformationType[] informationTypes)
        {
            Setup(HasExecuted());
            InformationTypes = informationTypes;
            return this;
        }

        /// <summary>
        /// Provided wheres will be gathered
        /// </summary>
        /// <param name="informationType">Information type</param>
        /// <param name="where">Where statement</param>
        /// <returns>Returns this with wheres</returns>
        public IDeviceCollection Where(InformationType informationType, string where)
        {
            Setup(HasExecuted());
            Queries.Add(new Query(informationType, where));

            if (InformationTypes == null)
            {
                InformationTypes = new[] { informationType };
            }
            InformationTypes = InformationTypes.Concat(new[] { informationType }).Distinct().ToArray();

            return this;
        }

        /// <summary>
        /// Provided queries will be gathered
        /// </summary>
        /// <param name="queries">List of query object containing InformationType and a string with where statement</param>
        /// <returns>Returns this with queries</returns>
        public IDeviceCollection WithQueries(IEnumerable<Query> queries)
        {
            Setup(HasExecuted());
            Queries.AddRange(queries);

            foreach (var query in Queries)
            {
                if (InformationTypes == null)
                {
                    InformationTypes = new[] { query.InformationType };
                }
                InformationTypes = InformationTypes.Concat(new[] { query.InformationType }).Distinct().ToArray();
            }

            return this;
        }

        /// <summary>
        /// Gets Device based on provided WMIAccessService
        /// </summary>
        /// <param name="wmiAccessService">Provides device access</param>
        /// <returns>Returns awaitable Task with Device</returns>
        public async Task<Device> GetDeviceAsync(WMIAccessService wmiAccessService)
        {
            Devices = await GetDevicesAsync(new[] { wmiAccessService }).ConfigureAwait(false);
            return Devices.FirstOrDefault();
        }

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        public async Task<List<Device>> GetDevicesAsync(params string[] hostNames)
        {
            Devices = await GetDevicesAsync(_accessProvider, hostNames).ConfigureAwait(false);
            return Devices;
        }

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        public async Task<List<Device>> GetDevicesAsync(IEnumerable<string> hostNames)
        {
            Devices = await GetDevicesAsync(_accessProvider, hostNames).ConfigureAwait(false);
            return Devices;
        }

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="credentialsProvider">Provides credential</param>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        public async Task<List<Device>> GetDevicesAsync(ICredentialsProvider credentialsProvider, params string[] hostNames)
        {
            Devices = await GetDevicesAsync(new AccessProvider(credentialsProvider), hostNames).ConfigureAwait(false);
            return Devices;
        }

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        public async Task<List<Device>> GetDevicesAsync(IEnumerable<string> hostNames, CancellationToken cancellationToken = new CancellationToken())
        {
            Devices = await GetDevicesAsync(_accessProvider, hostNames, cancellationToken).ConfigureAwait(false);
            return Devices;
        }

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="wmiAccessServices">Provides collection of device access</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        public async Task<List<Device>> GetDevicesAsync(IEnumerable<WMIAccessService> wmiAccessServices, CancellationToken cancellationToken = new CancellationToken())
        {
            var devices = new List<Device>();
            var deviceAdds = new List<Task>();
            foreach (var wmiAccessService in wmiAccessServices)
            {
                var task = Task.Run(async () =>
                {
                    var device = await TryGetDeviceAsync(wmiAccessService, cancellationToken, InformationCategories, InformationTypes).ConfigureAwait(false);
                    if (device != null)
                    {
                        devices.Add(device);
                    }
                }, cancellationToken);
                deviceAdds.Add(task);
            }
            await Task.WhenAll(deviceAdds).ConfigureAwait(false);
            Devices = devices;
            return Devices;
        }

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="accessProvider">Implementation of AccessProvider to provide access to devices</param>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        public async Task<List<Device>> GetDevicesAsync(AccessProvider accessProvider, IEnumerable<string> hostNames, CancellationToken cancellationToken = new CancellationToken())
        {
            if (accessProvider == null)
            {
                throw new ArgumentNullException(nameof(accessProvider));
            }

            var devices = new List<Device>();
            var deviceAdds = new List<Task>();
            foreach (var hostName in hostNames)
            {
                var task = Task.Run(async () =>
                {
                    var device = await TryGetDeviceAsync(accessProvider, hostName, cancellationToken, InformationCategories, InformationTypes).ConfigureAwait(false);
                    if (device != null)
                    {
                        devices.Add(device);
                    }
                }, cancellationToken);
                deviceAdds.Add(task);
            }
            await Task.WhenAll(deviceAdds).ConfigureAwait(false);
            Devices = devices;
            return Devices;
        }

        private async Task<Device> TryGetDeviceAsync(AccessProvider accessProvider, string hostName, CancellationToken cancellationToken = new CancellationToken(), InformationCategory[] informationCategories = null, params InformationType[] informationTypes)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (accessProvider != null)
                    {
                        var wmiAccessService = await accessProvider.GetAccessAsync(hostName).ConfigureAwait(false);
                        if (wmiAccessService != null)
                        {
                            return await TryGetDeviceAsync(wmiAccessService, cancellationToken, informationCategories, informationTypes).ConfigureAwait(false);
                        }
                    }
                }
                catch (OperationCanceledException operationCanceledException)
                {
                    LogEventHandler.TaskIncompleted(operationCanceledException);
                }
                catch (Exception exception)
                {
                    var wmiException = new WMIGeneralException(hostName, exception);
                    LogEventHandler.Exception(wmiException);
                }
                return null;
            }, cancellationToken).ConfigureAwait(false);
        }

        private async Task<Device> TryGetDeviceAsync(WMIAccessService wmiAccessService, CancellationToken cancellationToken = new CancellationToken(), InformationCategory[] informationCategories = null, params InformationType[] informationTypes)
        {
            if (wmiAccessService != null)
            {
                try
                {
                    return await Task.Run(async () =>
                    {
                        try
                        {
                            if (wmiAccessService.Connected)
                            {
                                var device = await new Device()
                                                        .WithWMIAccessService(wmiAccessService)
                                                        .WithInformationCategories(informationCategories)
                                                        .WithInformationTypes(informationTypes)
                                                        .WithQueries(Queries)
                                                        .InitializeAsync(cancellationToken)
                                                        .ConfigureAwait(false);
                                return device;
                            }
                        }
                        catch (OperationCanceledException operationCanceledException)
                        {
                            LogEventHandler.TaskIncompleted(operationCanceledException);
                        }
                        catch (Exception exception)
                        {
                            var wmiException = new WMIGeneralException(wmiAccessService.EndPoint, exception);
                            LogEventHandler.Exception(wmiException);
                        }
                        return null;
                    }, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException operationCanceledException)
                {
                    LogEventHandler.TaskIncompleted(operationCanceledException);
                }
                catch (Exception)
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                }
            }
            return null;
        }
    }
}
