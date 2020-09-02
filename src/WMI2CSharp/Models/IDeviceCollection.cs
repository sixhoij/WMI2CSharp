using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WMI2CSharp.Enums;
using WMI2CSharp.Providers;
using WMI2CSharp.Services;

namespace WMI2CSharp.Models
{
    /// <summary>
    /// Represents a collection of the type Device. Implements simplified functionality to gather multiple Devices at once
    /// </summary>
    public interface IDeviceCollection
    {
        List<Device> Devices { get; }

        List<Query> Queries { get; }

        DateTime Start { get; }

        int Properties { get; }

        int Exceptions { get; }

        int Connections { get; }

        int Warnings { get; }

        int TasksIncompleted { get; }

        InformationType[] InformationTypes { get; }

        InformationCategory[] InformationCategories { get; }

        /// <summary>
        /// Get a summary of the performance of the Device gathering
        /// </summary>
        /// <returns>Performance summary</returns>
        string Summarize();

        /// <summary>
        /// DeviceCollection to use the provided CredentialsProvider to get access
        /// </summary>
        /// <param name="credentialsProvider">Provides credential</param>
        /// <returns>Returns this with provided CredentialsProvider</returns>
        IDeviceCollection WithCredentials(ICredentialsProvider credentialsProvider);

        /// <summary>
        /// Only provided InformationCategories will be gathered
        /// </summary>
        /// <param name="informationCategories">Search categories</param>
        /// <returns>Returns this with search categories</returns>
        IDeviceCollection WithInformationCategories(params InformationCategory[] informationCategories);

        /// <summary>
        /// Only provided InformationTypes will be gathered
        /// </summary>
        /// <param name="informationTypes">Search types</param>
        /// <returns>Returns this with search types</returns>
        IDeviceCollection WithInformationTypes(params InformationType[] informationTypes);

        /// <summary>
        /// Provided wheres will be gathered
        /// </summary>
        /// <param name="informationType">Information type</param>
        /// <param name="where">Where statement</param>
        /// <returns>Returns this with wheres</returns>
        IDeviceCollection Where(InformationType informationType, string where);

        /// <summary>
        /// Provided queries will be gathered
        /// </summary>
        /// <param name="queries">List of query object containing InformationType and a string with where statement</param>
        /// <returns>Returns this with queries</returns>
        IDeviceCollection WithQueries(IEnumerable<Query> queries);


        /// <summary>
        /// Gets Device based on provided WMIAccessService
        /// </summary>
        /// <param name="wmiAccessService">Provides device access</param>
        /// <returns>Returns awaitable Task with Device</returns>
        Task<Device> GetDeviceAsync(WMIAccessService wmiAccessService);

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        Task<List<Device>> GetDevicesAsync(params string[] hostNames);

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        Task<List<Device>> GetDevicesAsync(IEnumerable<string> hostNames);

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="credentialsProvider">Provides credential</param>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        Task<List<Device>> GetDevicesAsync(ICredentialsProvider credentialsProvider, params string[] hostNames);

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        Task<List<Device>> GetDevicesAsync(IEnumerable<string> hostNames, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="wmiAccessServices">Provides collection of device access</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        Task<List<Device>> GetDevicesAsync(IEnumerable<WMIAccessService> wmiAccessServices, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Gets ICollection of Device
        /// </summary>
        /// <param name="accessProvider">Implementation of AccessProvider to provide access to devices</param>
        /// <param name="hostNames">To lookup and gather information</param>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with ICollection of Device</returns>
        Task<List<Device>> GetDevicesAsync(AccessProvider accessProvider, IEnumerable<string> hostNames, CancellationToken cancellationToken = new CancellationToken());
    }
}
