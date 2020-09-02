using System;
using System.Collections.Generic;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using WMI2CSharp.Exceptions;
using WMI2CSharp.Log;
using WMI2CSharp.Models;

namespace WMI2CSharp.Services
{
    public class WMIAccessService
    {
        private ManagementScope _managementScope;

        public bool Connected { get; private set; }

        public string EndPoint { get; private set; }

        public WMIAccessService() : this(Environment.MachineName)
        {
        }

        public WMIAccessService(string endPoint)
        {
            EndPoint = endPoint;
        }

        /// <summary>
        /// Tries to query WMI class from EndPoint.
        /// </summary>
        /// <param name="wmiClass">To query with WMI on EndPoint.</param>
        /// <returns>Returns awaitable Task with ManagementBaseObject.</returns>
        public async Task<ManagementBaseObject> TryQueryAsync(string wmiClass, string wmiWhereClause, CancellationToken cancellationToken)
        {
            try
            {
                var objectCollection = await TryGetObjectCollectionAsync(wmiClass, wmiWhereClause, cancellationToken).ConfigureAwait(false);
                if (objectCollection is ManagementObjectCollection managementObjectCollection)
                {
                    foreach (var managementObject in managementObjectCollection)
                    {
                        if (managementObject is ManagementBaseObject managementBaseObject)
                        {
                            return managementBaseObject;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogException(exception);
            }

            return null;
        }

        /// <summary>
        /// Tries to query a collection of WMI class from EndPoint.
        /// </summary>
        /// <param name="wmiClass">To query with WMI on EndPoint.</param>
        /// <returns>Returns awaitable Task with IEnumerable of ManagementBaseObject.</returns>
        public async Task<IEnumerable<ManagementBaseObject>> TryQueryCollectionAsync(string wmiClass, string wmiWhereClause, CancellationToken cancellationToken)
        {
            var objectList = new List<ManagementBaseObject>();
            try
            {
                var objectCollection = await TryGetObjectCollectionAsync(wmiClass, wmiWhereClause, cancellationToken).ConfigureAwait(false);
                if (objectCollection is ManagementObjectCollection managementObjectCollection)
                {
                    foreach (var managementObject in managementObjectCollection)
                    {
                        if (managementObject is ManagementBaseObject managementBaseObject)
                        {
                            objectList.Add(managementBaseObject);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogException(exception);
            }

            return objectList;
        }

        /// <summary>
        /// Tries to connect.
        /// </summary>
        /// <param name="wmiConnectionOption">To try to connect.</param>
        /// <returns>Returns awaitable Task with bool state of connection.</returns>
        public async Task<bool> TryConnectAsync(WMIConnectionOption wmiConnectionOption)
        {
            await TryConnectAsync(wmiConnectionOption?.EndPoint, wmiConnectionOption?.ConnectionOptions).ConfigureAwait(false);
            return Connected;
        }

        /// <summary>
        /// Tries to connect.
        /// </summary>
        /// <param name="user">UserName</param>
        /// <param name="pass">Password</param>
        /// <param name="endPoint">EndPoint</param>
        /// <param name="domain">Domain</param>
        /// <returns>Returns awaitable Task with bool state of connection.</returns>
        public async Task<bool> TryConnectAsync(string user, string pass, string endPoint, string domain)
        {
            ConnectionOptions connectionOptions = null;
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(domain))
            {
                connectionOptions = new ConnectionOptions
                {
                    Username = user,
                    Password = pass,
                    Authority = $"ntlmdomain:{domain.ToLower()}"
                };
            }
            await TryConnectAsync(endPoint, connectionOptions).ConfigureAwait(false);
            return Connected;
        }

        /// <summary>
        /// Tries to connect to localhost.
        /// </summary>
        /// <returns>Returns awaitable Task with bool state of connection.</returns>
        public async Task<bool> TryConnectAsync()
        {
            await TryConnectAsync(EndPoint, null).ConfigureAwait(false);
            return Connected;
        }

        private async Task TryConnectAsync(string endPoint, ConnectionOptions connectionOptions)
        {
            await Task.Run(() =>
            {
                EndPoint = endPoint;
                if (string.IsNullOrEmpty(endPoint))
                {
                    Connected = false;
                }
                else
                {
                    try
                    {
                        _managementScope = connectionOptions == null || EndPoint.ToLower() == "local" || EndPoint == "."
                            ? new ManagementScope($@"\\{EndPoint}\root\CIMV2")
                            : new ManagementScope($@"\\{EndPoint}\root\CIMV2", connectionOptions);
                        ConnectionGuard();
                    }
                    catch (Exception exception)
                    {
                        LogException(exception);
                    }
                }
            }).ConfigureAwait(false);
        }

        private async Task<ManagementObjectCollection> TryGetObjectCollectionAsync(string wmiClass, string wmiWhereClause, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    ConnectionGuard();
                    if (Connected)
                    {
                        var wmiWhere = string.IsNullOrEmpty(wmiWhereClause) ? string.Empty : wmiWhereClause;
                        var objectQuery = new ObjectQuery($"SELECT * FROM {wmiClass} {wmiWhere}");
                        var objectSearcher = new ManagementObjectSearcher(_managementScope, objectQuery);
                        var objectCollection = objectSearcher.Get();
                        return objectCollection;
                    }
                }
                catch (Exception exception)
                {
                    LogException(exception);
                }
                return null;
            }, cancellationToken).ConfigureAwait(false);
        }

        private void ConnectionGuard()
        {
            if (!Connected)
            {
                try
                {
                    _managementScope.Connect();
                    Connected = true;
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    Connected = false;
                }
                var connectedText = Connected ? "Connected" : "Disconnected";
                LogEventHandler.Connection($"{EndPoint}: {connectedText}");
            }
        }

        private void LogException(Exception exception)
        {
            var data = string.Empty;
            if (exception is ManagementException managementException)
            {
                var parameterInfo = managementException.ErrorInformation.Properties["ParameterInfo"].Value.ToString();
                if (!string.IsNullOrEmpty(parameterInfo))
                {
                    data += parameterInfo;
                }
            }

            if (string.IsNullOrEmpty(data))
            {
                data = exception.Message;
            }

            var wmiException = new WMIGeneralException(EndPoint, data, exception);
            LogEventHandler.Exception(wmiException);
        }
    }
}
