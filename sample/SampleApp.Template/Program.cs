using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using WMI2CSharp.Enums;
using WMI2CSharp.Log;
using WMI2CSharp.Models;
using WMI2CSharp.Providers;

namespace SampleApp.Template
{
    internal class Program
    {
        private static int _properties;
        private static int _exceptions;
        private static int _connections;
        private static int _warnings;
        private static DateTime _start;
        private static ICollection<Exception> _exceptionMessages;
        private static ICollection<string> _warningMessages;

        private static async Task Main(string[] args)
        {
            Setup();

            var searchInformationCategories = new[]
            {
                InformationCategory.OperatingSystem,
                InformationCategory.InternalHardware,
                InformationCategory.Network,
                InformationCategory.Software,
                InformationCategory.System,
                InformationCategory.User,
                InformationCategory.Application
            };

            var searchInformationTypes = new[]
            {
                InformationType.OperatingSystem,
                InformationType.SerialPorts
            };

            var hostNames = new[] { "HostName1", "HostName2", "HostName3" };
            var devices = new List<Device>(await new DeviceCollection()
                                                    .WithCredentials(new CustomCredentialsProvider())
                                                    .WithInformationCategories(searchInformationCategories)
                                                    .WithInformationTypes(searchInformationTypes)
                                                    .GetDevicesAsync(hostNames)
                                                    .ConfigureAwait(false));

            if (devices.Any())
            {
                Summarize(devices);
            }

            Console.ReadLine();
        }

        private static void Summarize(ICollection<Device> devices)
        {
            var executionTime = DateTime.Now - _start;
            var combinedTotalRunTime = TimeSpan.Zero;
            devices.ToList().ForEach(device => combinedTotalRunTime += device.ExecutionTime);

            var avgTime = combinedTotalRunTime.TotalSeconds / devices.Count;
            var fastestTime = TimeSpan.Zero;
            var slowestTime = TimeSpan.Zero;
            Device fastestDevice = null;
            Device slowestDevice = null;
            var belowAvg = 0;
            var aboveAvg = 0;

            foreach (var device in devices.OrderBy(x => x.ExecutionTime))
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
                    Console.WriteLine($"Execution Time : {device.ExecutionTime} => {device.EndPoint}");
                    aboveAvg++;
                }
                if (device.ExecutionTime.TotalSeconds < avgTime)
                {
                    belowAvg++;
                }
            }

            Console.WriteLine($"Fetched {devices.Count} devices!");
            Console.WriteLine($"Fetched {_properties} properties!");
            Console.WriteLine($"Catched {_exceptions} exceptions!");
            Console.WriteLine($"Catched {_warnings} warnings!");
            Console.WriteLine($"Resolved {_connections} connections!");
            Console.WriteLine($"Execution Time : {executionTime}");
            Console.WriteLine($"Combined Total Run Time : {combinedTotalRunTime}");
            Console.WriteLine($"Time Saved by running devices async: {combinedTotalRunTime - executionTime}");
            Console.WriteLine($"Seconds per device : {executionTime.TotalSeconds / devices.Count}");
            Console.WriteLine($"Seconds per property : {executionTime.TotalSeconds / _properties}");
            Console.WriteLine($"Properties per second : {_properties / executionTime.TotalSeconds}");
            Console.WriteLine($"Fastest Execution Time : {fastestDevice?.ExecutionTime} => {fastestDevice?.EndPoint}");
            Console.WriteLine($"Slowest Execution Time : {slowestDevice?.ExecutionTime} => {slowestDevice?.EndPoint}");
            Console.WriteLine($"AvgTime : {TimeSpan.FromSeconds(avgTime)}");
            Console.WriteLine($"Difference : {slowestDevice?.ExecutionTime - fastestDevice?.ExecutionTime}");
            if (slowestDevice != null && fastestDevice != null)
            {
                Console.WriteLine($"Calculated AvgTime : {TimeSpan.FromSeconds((slowestDevice.ExecutionTime.TotalSeconds + fastestDevice.ExecutionTime.TotalSeconds) / 2)}");
            }
            Console.WriteLine($"BelowAvg Count : {belowAvg}");
            Console.WriteLine($"AboveAvg Count : {aboveAvg}");
        }

        private static void Setup()
        {
            _properties = 0;
            _exceptions = 0;
            _connections = 0;
            _warnings = 0;
            _exceptionMessages = new List<Exception>();
            _warningMessages = new List<string>();
            LogEventHandler.OnInformationMessage += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Object);
                _properties++;
            };
            LogEventHandler.OnExceptionMessage += (sender, eventArgs) =>
            {
                Console.WriteLine($"EXCEPTION: {eventArgs.Object}");
                _exceptionMessages.Add(eventArgs.Object);
                _exceptions++;
            };
            LogEventHandler.OnConnectionMessage += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Object}");
                _connections++;
            };
            LogEventHandler.OnWarningMessage += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Object}");
                _warningMessages.Add(eventArgs.Object);
                _warnings++;
            };
            Console.ForegroundColor = ConsoleColor.Green;
            _start = DateTime.Now;
        }
    }

    internal class CustomCredentialsProvider : ICredentialsProvider
    {
        private readonly string _domain;

        public CustomCredentialsProvider() : this(null)
        {
        }

        public CustomCredentialsProvider(string domain)
        {
            _domain = domain;
        }

        public async Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName)
        {
            return await GetWMIConnectionOptionAsync(hostName, _domain).ConfigureAwait(false);
        }

        public async Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName, string domain)
        {
            return await Task.Run(async () =>
            {
                if (string.IsNullOrEmpty(domain))
                {
                    domain = await GetDomainAsync(hostName).ConfigureAwait(false);
                }

                return !string.IsNullOrEmpty(domain)
                    ? new WMIConnectionOption(hostName, domain, hostName, GetPassword(hostName))
                    : null;
            }).ConfigureAwait(false);
        }

        private async Task<string> GetDomainAsync(string hostName)
        {
            return await DomainChecker.TryGetDomainAsync(hostName).ConfigureAwait(false);
        }

        private string GetPassword(string hostName)
        {
            return $"{hostName}+Password";
        }
    }

    internal static class DomainChecker
    {
        public static readonly ICollection<string> Domains = new[] { "custom.domain1.com", "custom.domain2.com", "custom.domain3.com" };

        public static async Task<string> TryGetDomainAsync(string hostName)
        {
            return await Task.Run(async () =>
            {
                foreach (var domain in Domains)
                {
                    var pingInput = $"{hostName}.{domain}";
                    try
                    {
                        var reply = await new Ping().SendPingAsync(pingInput).ConfigureAwait(false);
                        if (reply.Status == IPStatus.Success && reply.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return domain;
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                return string.Empty;
            }).ConfigureAwait(false);
        }
    }
}
