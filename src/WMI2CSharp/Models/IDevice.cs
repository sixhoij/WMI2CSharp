using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WMI2CSharp.Enums;
using WMI2CSharp.Services;

namespace WMI2CSharp.Models
{
    public interface IDevice
    {
        List<Query> Queries { get; }

        /// <summary>
        /// Device to use the provided WMIAccessService as access
        /// </summary>
        /// <param name="wmiAccessService">Provides access</param>
        /// <returns>Returns this with provided WMIAccessService</returns>
        IDevice WithWMIAccessService(WMIAccessService wmiAccessService);

        /// <summary>
        /// Provided InformationCategories will be gathered
        /// </summary>
        /// <param name="searchInformationCategories">Search categories</param>
        /// <returns>Returns this with search categories</returns>
        IDevice WithInformationCategories(params InformationCategory[] searchInformationCategories);

        /// <summary>
        /// Provided InformationTypes will be gathered
        /// </summary>
        /// <param name="searchInformationTypes">Search types</param>
        /// <returns>Returns this with search types</returns>
        IDevice WithInformationTypes(params InformationType[] searchInformationTypes);

        /// <summary>
        /// Provided queries will be gathered
        /// </summary>
        /// <param name="queries">List of query object containing InformationType and a string with where statement</param>
        /// <returns>Returns this with queries</returns>
        IDevice WithQueries(IEnumerable<Query> queries);

        /// <summary>
        /// Provided wheres will be gathered
        /// </summary>
        /// <param name="informationType">Information type</param>
        /// <param name="where">Where statement</param>
        /// <returns>Returns this with wheres</returns>
        IDevice Where(InformationType informationType, string where);

        /// <summary>
        /// Initialize information gathering, when method is finished, properties of this Device will be filled with the gathered data.
        /// </summary>
        /// <returns>Returns awaitable Task with this Device</returns>
        Task<Device> InitializeAsync();

        /// <summary>
        /// Initialize information gathering, when method is finished, properties of this Device will be filled with the gathered data
        /// </summary>
        /// <param name="cancellationToken">To cancel the Tasks started by calling the method</param>
        /// <returns>Returns awaitable Task with this Device</returns>
        Task<Device> InitializeAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
