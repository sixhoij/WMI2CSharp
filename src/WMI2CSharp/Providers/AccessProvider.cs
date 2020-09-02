using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WMI2CSharp.Exceptions;
using WMI2CSharp.Log;
using WMI2CSharp.Models;
using WMI2CSharp.Services;

namespace WMI2CSharp.Providers
{
    /// <summary>
    /// Represents a provider to get access to devices.
    /// </summary>
    public class AccessProvider
    {
        private readonly ICredentialsProvider _credentialsProvider;

        public AccessProvider(ICredentialsProvider credentialsProvider)
        {
            _credentialsProvider = credentialsProvider;
        }

        /// <summary>
        /// Gets ICollection of WMIAccessService for WMIConnectionOptions that have access.
        /// </summary>
        /// <param name="wmiConnectionOptions">To test access.</param>
        /// <returns>Returns awaitable Task with ICollection of WMIAccessService.</returns>
        public async Task<ICollection<WMIAccessService>> GetAccessAsync(IEnumerable<WMIConnectionOption> wmiConnectionOptions)
        {
            var tasks = new List<Task>();
            var connectedWMIAccessServices = new List<WMIAccessService>();

            foreach (var wmiConnectionOption in wmiConnectionOptions)
            {
                var result = Task.Run(async () => connectedWMIAccessServices.Add(await TryGetAccessAsync(wmiConnectionOption).ConfigureAwait(false)));
                tasks.Add(result);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return connectedWMIAccessServices;
        }

        /// <summary>
        /// Gets ICollection of WMIAccessService for hostNames that have access.
        /// </summary>
        /// <param name="hostNames">To test access.</param>
        /// <returns>Returns awaitable Task with ICollection of WMIAccessService.</returns>
        public async Task<ICollection<WMIAccessService>> GetAccessAsync(IEnumerable<string> hostNames)
        {
            var wmiConnectionOptions = new List<WMIConnectionOption>();
            foreach (var hostName in hostNames)
            {
                var wmiConnectionOption = await GetConnectionOptionAsync(hostName).ConfigureAwait(false);
                wmiConnectionOptions.Add(wmiConnectionOption);
            }
            return await GetAccessAsync(wmiConnectionOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets WMIAccessService for hostName that have access.
        /// </summary>
        /// <param name="hostName">To test access.</param>
        /// <returns>Returns awaitable Task with WMIAccessService.</returns>
        public async Task<WMIAccessService> GetAccessAsync(string hostName)
        {
            var connectionOption = await GetConnectionOptionAsync(hostName).ConfigureAwait(false);
            return await GetAccessAsync(connectionOption).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets WMIAccessService for WMIConnectionOption that have access.
        /// </summary>
        /// <param name="wmiConnectionOption">To test access.</param>
        /// <returns>Returns awaitable Task with WMIAccessService.</returns>
        public async Task<WMIAccessService> GetAccessAsync(WMIConnectionOption wmiConnectionOption)
        {
            return await TryGetAccessAsync(wmiConnectionOption).ConfigureAwait(false);
        }

        private Task<WMIAccessService> TryGetAccessAsync(WMIConnectionOption wmiConnectionOption)
        {
            return Task.Run(async () =>
            {
                WMIAccessService connectedWMIConnectedAccessService = null;
                if (wmiConnectionOption != null)
                {
                    try
                    {
                        var wmiAccessService = new WMIAccessService();
                        var connected = await wmiAccessService.TryConnectAsync(wmiConnectionOption).ConfigureAwait(false);
                        if (connected)
                        {
                            connectedWMIConnectedAccessService = wmiAccessService;
                        }
                    }
                    catch (Exception exception)
                    {
                        var endPoint = string.IsNullOrEmpty(wmiConnectionOption.EndPoint)
                            ? wmiConnectionOption.DeviceName
                            : wmiConnectionOption.EndPoint;
                        var wmiException = new WMIGeneralException(endPoint, exception);
                        LogEventHandler.Exception(wmiException);
                    }
                }
                return connectedWMIConnectedAccessService;
            });
        }

        private async Task<WMIConnectionOption> GetConnectionOptionAsync(string hostName)
        {
            var wmiConnectionOptions = await _credentialsProvider.GetWMIConnectionOptionAsync(hostName).ConfigureAwait(false);
            return wmiConnectionOptions;
        }
    }
}
