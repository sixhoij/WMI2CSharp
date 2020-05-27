using System;
using System.Management;

namespace WMI2CSharp.Models
{
    /// <summary>
    /// Represents connection information to access WMI (Windows Management Instrumentation) on Device 
    /// </summary>
    public class WMIConnectionOption
    {
        public string DeviceName { get; set; }

        public string DeviceDomain { get; set; }

        public string EndPoint { get; set; }

        public ConnectionOptions ConnectionOptions { get; set; }

        public WMIConnectionOption() : this(Environment.MachineName)
        {
        }

        public WMIConnectionOption(string deviceName, string deviceDomain = "local") : this(deviceName, deviceDomain, null)
        {
        }

        public WMIConnectionOption(string deviceName, string deviceDomain, ConnectionOptions connectionOptions)
        {
            Initialize(deviceName, deviceDomain, connectionOptions);
        }

        public WMIConnectionOption(string deviceName, string deviceDomain, string user, string pass)
        {
            var connectionOptions = !string.IsNullOrEmpty(deviceDomain)
                ? new ConnectionOptions
                {
                    Username = user,
                    Password = pass,
                    Authority = "ntlmdomain:" + deviceDomain.ToLower()
                }
                : new ConnectionOptions
                {
                    Username = user,
                    Password = pass
                };
            Initialize(deviceName, deviceDomain, connectionOptions);
        }

        private void Initialize(string deviceName, string deviceDomain, ConnectionOptions connectionOptions)
        {
            ConnectionOptions = connectionOptions;
            DeviceName = deviceName;
            DeviceDomain = deviceDomain;
            EndPoint = string.IsNullOrEmpty(DeviceDomain) ? $"{DeviceName}" : $"{DeviceName}.{DeviceDomain}";
        }

        /// <summary>
        /// Gets EndPoint
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return EndPoint;
        }
    }
}
