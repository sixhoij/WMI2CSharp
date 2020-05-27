using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WMI2CSharp.Enums;
using WMI2CSharp.Log;
using WMI2CSharp.Providers;
using WMI2CSharp.Services;

namespace WMI2CSharp.Models
{
    /// <summary>
    /// Represents a collection of the type Device. Implements simplified functionality to gather multiple Devices at once.
    /// </summary>
    public class DeviceCollection : IDeviceCollection
    {
        private AccessProvider _accessProvider;

        public InformationType[] InformationTypes { get; private set; }

        public InformationCategory[] InformationCategories { get; private set; }

        public DeviceCollection(ICredentialsProvider credentialsProvider, InformationCategory[] informationCategories = null, params InformationType[] informationTypes)
        {
            _accessProvider = new AccessProvider(credentialsProvider);
            InformationCategories = informationCategories;
            InformationTypes = informationTypes;
        }

        public DeviceCollection(InformationCategory[] informationCategories = null, params InformationType[] informationTypes)
        {
            InformationCategories = informationCategories;
            InformationTypes = informationTypes;
        }

        public DeviceCollection()
        {
        }

        /// <summary>
        /// DeviceCollection to use the provided CredentialsProvider to get access.
        /// </summary>
        /// <param name="credentialsProvider">Provides credential.</param>
        /// <returns>Returns this with provided CredentialsProvider.</returns>
        public IDeviceCollection WithCredentials(ICredentialsProvider credentialsProvider)
        {
            _accessProvider = new AccessProvider(credentialsProvider);
            return this;
        }

        /// <summary>
        /// Only provided InformationCategories will be gathered.
        /// </summary>
        /// <param name="informationCategories">Search categories.</param>
        /// <returns>Returns this with search categories.</returns>
        public IDeviceCollection WithInformationCategories(params InformationCategory[] informationCategories)
        {
            InformationCategories = informationCategories;
            return this;
        }

        /// <summary>
        /// Only provided InformationTypes will be gathered.
        /// </summary>
        /// <param name="informationTypes">Search types.</param>
        /// <returns>Returns this with search types.</returns>
        public IDeviceCollection WithInformationTypes(params InformationType[] informationTypes)
        {
            InformationTypes = informationTypes;
            return this;
        }

        /// <summary>
        /// Gets Device based on provided WMIAccessService.
        /// </summary>
        /// <param name="wmiAccessService">Provides device access.</param>
        /// <returns>Returns awaitable Task with Device.</returns>
        public async Task<Device> GetDeviceAsync(WMIAccessService wmiAccessService)
        {
            var devices = await GetDevicesAsync(new[] { wmiAccessService }).ConfigureAwait(false);
            return devices.FirstOrDefault();
        }

        /// <summary>
        /// Gets ICollection of Device.
        /// </summary>
        /// <param name="hostNames">To lookup and gather information.</param>
        /// <returns>Returns awaitable Task with ICollection of Device.</returns>
        public async Task<ICollection<Device>> GetDevicesAsync(params string[] hostNames)
        {
            return await GetDevicesAsync(_accessProvider, hostNames).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets ICollection of Device.
        /// </summary>
        /// <param name="hostNames">To lookup and gather information.</param>
        /// <returns>Returns awaitable Task with ICollection of Device.</returns>
        public async Task<ICollection<Device>> GetDevicesAsync(IEnumerable<string> hostNames)
        {
            return await GetDevicesAsync(_accessProvider, hostNames).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets ICollection of Device.
        /// </summary>
        /// <param name="credentialsProvider">Provides credential.</param>
        /// <param name="hostNames">To lookup and gather information.</param>
        /// <returns>Returns awaitable Task with ICollection of Device.</returns>
        public async Task<ICollection<Device>> GetDevicesAsync(ICredentialsProvider credentialsProvider, params string[] hostNames)
        {
            return await GetDevicesAsync(new AccessProvider(credentialsProvider), hostNames).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets ICollection of Device.
        /// </summary>
        /// <param name="hostNames">To lookup and gather information.</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method.</param>
        /// <returns>Returns awaitable Task with ICollection of Device.</returns>
        public async Task<ICollection<Device>> GetDevicesAsync(IEnumerable<string> hostNames, CancellationToken cancellationToken)
        {
            return await GetDevicesAsync(_accessProvider, hostNames, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets ICollection of Device.
        /// </summary>
        /// <param name="wmiAccessServices">Provides collection of device access.</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method.</param>
        /// <returns>Returns awaitable Task with ICollection of Device.</returns>
        public async Task<ICollection<Device>> GetDevicesAsync(IEnumerable<WMIAccessService> wmiAccessServices, CancellationToken cancellationToken = new CancellationToken())
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
            return devices;
        }

        /// <summary>
        /// Gets ICollection of Device.
        /// </summary>
        /// <param name="accessProvider">Implementation of AccessProvider to provide access to devices.</param>
        /// <param name="hostNames">To lookup and gather information.</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method.</param>
        /// <returns>Returns awaitable Task with ICollection of Device.</returns>
        public async Task<ICollection<Device>> GetDevicesAsync(AccessProvider accessProvider, IEnumerable<string> hostNames, CancellationToken cancellationToken = new CancellationToken())
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
            return devices;
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
                catch (Exception exception)
                {
                    LogEventHandler.Exception(exception);
                }
                return null;
            }, cancellationToken).ConfigureAwait(false);
        }

        private async Task<Device> TryGetDeviceAsync(WMIAccessService wmiAccessService, CancellationToken cancellationToken = new CancellationToken(), InformationCategory[] informationCategories = null, params InformationType[] informationTypes)
        {
            if (wmiAccessService != null)
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
                                                    .InitializeAsync()
                                                    .ConfigureAwait(false);
                            return device;
                        }
                    }
                    catch (Exception exception)
                    {
                        LogEventHandler.Exception(exception);
                    }
                    return null;
                }, cancellationToken).ConfigureAwait(false);
            }
            return null;
        }
    }
}
