using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMI2CSharp.Enums;
using WMI2CSharp.Models;
using WMI2CSharp.Models.DeviceModels;
using WMI2CSharp.Services;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace WMI2CSharp.Test.Integrationtest
{
    [TestClass]
    public class Integrationtests
    {
        private Device _device;

        [TestCleanup]
        public void Cleanup()
        {
            AssertFetchingDevice(_device);
        }

        [TestMethod]
        public async Task FetchOperatingSystemDataFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.OperatingSystem
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            device.OperatingSystem.Should().NotBeNull();
            device.OperatingSystem.Caption.Should().NotBeNullOrEmpty();
            device.OperatingSystem.Version.Should().NotBeNullOrEmpty();
            device.OperatingSystem.BuildNumber.Should().NotBeNullOrEmpty();
            device.OperatingSystem.InstallDate.Should().NotBeNullOrEmpty();
            device.OperatingSystem.Name.Should().NotBeNullOrEmpty();
            device.OperatingSystem.Manufacturer.Should().NotBeNullOrEmpty();
            device.OperatingSystem.NumberOfProcesses.Should().BeGreaterOrEqualTo(1);
            device.OperatingSystem.NumberOfUsers.Should().BeGreaterOrEqualTo(1);
            device.OperatingSystem.FreePhysicalMemory.Should().BeGreaterOrEqualTo(1 * 1024 * 1024);
            device.BIOS.Should().BeEquivalentTo(new BIOS());
            device.BaseBoard.Should().BeEquivalentTo(new BaseBoard());
            device.ComputerSystem.Should().BeEquivalentTo(new ComputerSystem());
            device.Desktops.Should().BeEquivalentTo(new List<Desktop>());
            device.DiskDrives.Should().BeEquivalentTo(new List<DiskDrive>());
            device.DiskPartitions.Should().BeEquivalentTo(new List<DiskPartition>());
            device.Disks.Should().BeEquivalentTo(new List<Disk>());
            device.Environments.Should().BeEquivalentTo(new List<Models.DeviceModels.Environment>());
            device.MappedDisks.Should().BeEquivalentTo(new List<MappedDisk>());
            device.NetworkAdapterConfigurations.Should().BeEquivalentTo(new List<NetworkAdapterConfiguration>());
            device.NetworkAdapters.Should().BeEquivalentTo(new List<NetworkAdapter>());
            device.PNPEntities.Should().BeEquivalentTo(new List<PNPEntity>());
            device.PNPSignedDrivers.Should().BeEquivalentTo(new List<PNPSignedDriver>());
            device.PhysicalMemories.Should().BeEquivalentTo(new List<PhysicalMemory>());
            device.PrinterConfigurations.Should().BeEquivalentTo(new List<PrinterConfiguration>());
            device.Printers.Should().BeEquivalentTo(new List<Printer>());
            device.Processes.Should().BeEquivalentTo(new List<Process>());
            device.Processor.Should().BeEquivalentTo(new Processor());
            device.Products.Should().BeEquivalentTo(new List<Product>());
            device.QuickFixEngineerings.Should().BeEquivalentTo(new List<QuickFixEngineering>());
            device.SerialPortConfigurations.Should().BeEquivalentTo(new List<SerialPortConfiguration>());
            device.SerialPorts.Should().BeEquivalentTo(new List<SerialPort>());
            device.Services.Should().BeEquivalentTo(new List<Service>());
            device.SoftwareLicensingService.Should().BeEquivalentTo(new SoftwareLicensingService());
            device.TimeZone.Should().BeEquivalentTo(new Models.DeviceModels.TimeZone());
            device.VideoControllers.Should().BeEquivalentTo(new List<VideoController>());
            device.Volumes.Should().BeEquivalentTo(new List<Volume>());
            device.WindowsScore.Should().BeEquivalentTo(new WindowsScore());
        }

        [TestMethod]
        public async Task FetchBIOSFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.BIOS
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertBIOS(device);
        }

        private static void AssertBIOS(Device device)
        {
            device.BIOS.Should().NotBeNull();
            device.BIOS.Should().NotBeEquivalentTo(new BIOS());
        }

        [TestMethod]
        public async Task FetchBaseBoardFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.BaseBoard
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertBaseBoard(device);
        }

        private static void AssertBaseBoard(Device device)
        {
            device.BaseBoard.Should().NotBeNull();
            device.BaseBoard.Should().NotBeEquivalentTo(new BaseBoard());
        }

        [TestMethod]
        public async Task FetchComputerSystemFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.ComputerSystem
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertComputerSystem(device);
        }

        private static void AssertComputerSystem(Device device)
        {
            device.ComputerSystem.Should().NotBeNull();
            device.ComputerSystem.Should().NotBeEquivalentTo(new ComputerSystem());
        }

        [TestMethod]
        public async Task FetchDesktopsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Desktops
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertDesktops(device);
        }

        private static void AssertDesktops(Device device)
        {
            device.Desktops.Should().NotBeNull();
            device.Desktops.Should().NotContainNulls();
            device.Desktops.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchDiskDrivesFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.DiskDrives
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertDiskDrives(device);
        }

        private static void AssertDiskDrives(Device device)
        {
            device.DiskDrives.Should().NotBeNull();
            device.DiskDrives.Should().NotContainNulls();
            device.DiskDrives.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchDiskPartitionsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.DiskPartitions
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertDiskPartitions(device);
        }

        private static void AssertDiskPartitions(Device device)
        {
            device.DiskPartitions.Should().NotBeNull();
            device.DiskPartitions.Should().NotContainNulls();
            device.DiskPartitions.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchDisksFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Disks
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertDisks(device);
        }

        private static void AssertDisks(Device device)
        {
            device.Disks.Should().NotBeNull();
            device.Disks.Should().NotContainNulls();
            device.Disks.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchEnvironmentsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Environments
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertEnvironments(device);
        }

        private static void AssertEnvironments(Device device)
        {
            device.Environments.Should().NotBeNull();
            device.Environments.Should().NotContainNulls();
            device.Environments.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchLocalTimeFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.LocalTime
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertLocalTime(device);
        }

        private static void AssertLocalTime(Device device)
        {
            device.LocalTime.Should().NotBeNull();
            device.LocalTime.Should().NotBeEquivalentTo(new LocalTime());
        }

        [TestMethod]
        public async Task FetchMappedDisksFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.MappedDisks
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertMappedDisks(device);
        }

        private static void AssertMappedDisks(Device device)
        {
            device.MappedDisks.Should().NotBeNull();
            device.MappedDisks.Should().NotContainNulls();
            device.MappedDisks.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchNetworkAdapterConfigurationsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.NetworkAdapterConfigurations
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertNetworkAdapterConfigurations(device);
        }

        private static void AssertNetworkAdapterConfigurations(Device device)
        {
            device.NetworkAdapterConfigurations.Should().NotBeNull();
            device.NetworkAdapterConfigurations.Should().NotContainNulls();
            device.NetworkAdapterConfigurations.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchNetworkAdaptersFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.NetworkAdapters
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertNetworkAdapters(device);
        }

        private static void AssertNetworkAdapters(Device device)
        {
            device.NetworkAdapters.Should().NotBeNull();
            device.NetworkAdapters.Should().NotContainNulls();
            device.NetworkAdapters.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchOperatingSystemFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.OperatingSystem
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertOperatingSystem(device);
        }

        private static void AssertOperatingSystem(Device device)
        {
            device.OperatingSystem.Should().NotBeNull();
            device.OperatingSystem.Should().NotBeEquivalentTo(new Models.DeviceModels.OperatingSystem());
        }

        [TestMethod]
        public async Task FetchPNPEntitiesFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.PNPEntities
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertPNPEntities(device);
        }

        private static void AssertPNPEntities(Device device)
        {
            device.PNPEntities.Should().NotBeNull();
            device.PNPEntities.Should().NotContainNulls();
            device.PNPEntities.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchPNPSignedDriversFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.PNPSignedDrivers
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertPNPSignedDrivers(device);
        }

        private static void AssertPNPSignedDrivers(Device device)
        {
            device.PNPSignedDrivers.Should().NotBeNull();
            device.PNPSignedDrivers.Should().NotContainNulls();
            device.PNPSignedDrivers.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchPhysicalMemoriesFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.PhysicalMemories
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertPhysicalMemories(device);
        }

        private static void AssertPhysicalMemories(Device device)
        {
            device.PhysicalMemories.Should().NotBeNull();
            device.PhysicalMemories.Should().NotContainNulls();
            device.PhysicalMemories.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchPrinterConfigurationsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.PrinterConfigurations
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertPrinterConfigurations(device);
        }

        private static void AssertPrinterConfigurations(Device device)
        {
            device.PrinterConfigurations.Should().NotBeNull();
            device.PrinterConfigurations.Should().NotContainNulls();
            device.PrinterConfigurations.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchPrintersFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Printers
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertPrinters(device);
        }

        private static void AssertPrinters(Device device)
        {
            device.Printers.Should().NotBeNull();
            device.Printers.Should().NotContainNulls();
            device.Printers.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchProcessesFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Processes
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertProcesses(device);
        }

        private static void AssertProcesses(Device device)
        {
            device.Processes.Should().NotBeNull();
            device.Processes.Should().NotContainNulls();
            device.Processes.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchProcessorFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Processor
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertProcessor(device);
        }

        private static void AssertProcessor(Device device)
        {
            device.Processor.Should().NotBeNull();
            device.Processor.Should().NotBeEquivalentTo(new Processor());
        }

        [TestMethod]
        public async Task FetchProductsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Products
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertProducts(device);
        }

        private static void AssertProducts(Device device)
        {
            device.Products.Should().NotBeNull();
            device.Products.Should().NotContainNulls();
            device.Products.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchQuickFixEngineeringsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.QuickFixEngineerings
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertQuickFixEngineerings(device);
        }

        private static void AssertQuickFixEngineerings(Device device)
        {
            device.QuickFixEngineerings.Should().NotBeNull();
            device.QuickFixEngineerings.Should().NotContainNulls();
            device.QuickFixEngineerings.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchSerialPortConfigurationsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.SerialPortConfigurations
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertSerialPortConfigurations(device);
        }

        private static void AssertSerialPortConfigurations(Device device)
        {
            device.SerialPortConfigurations.Should().NotBeNull();
            device.SerialPortConfigurations.Should().NotContainNulls();
            device.SerialPortConfigurations.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchSerialPortsFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.SerialPorts
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertSerialPorts(device);
        }

        private static void AssertSerialPorts(Device device)
        {
            device.SerialPorts.Should().NotBeNull();
            device.SerialPorts.Should().NotContainNulls();
            device.SerialPorts.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchServicesFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Services
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertServices(device);
        }

        private static void AssertServices(Device device)
        {
            device.Services.Should().NotBeNull();
            device.Services.Should().NotContainNulls();
            device.Services.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchSoftwareLicensingServiceFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.SoftwareLicensingService
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertSoftwareLicensingService(device);
        }

        private static void AssertSoftwareLicensingService(Device device)
        {
            device.SoftwareLicensingService.Should().NotBeNull();
            device.SoftwareLicensingService.Should().NotBeEquivalentTo(new SoftwareLicensingService());
        }

        [TestMethod]
        public async Task FetchTimeZoneFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.TimeZone
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertTimeZone(device);
        }

        private static void AssertTimeZone(Device device)
        {
            device.TimeZone.Should().NotBeNull();
            device.TimeZone.Should().NotBeEquivalentTo(new Models.DeviceModels.TimeZone());
        }

        [TestMethod]
        public async Task FetchVideoControllersFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.VideoControllers
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertVideoControllers(device);
        }

        private static void AssertVideoControllers(Device device)
        {
            device.VideoControllers.Should().NotBeNull();
            device.VideoControllers.Should().NotContainNulls();
            device.VideoControllers.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchVolumesFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.Volumes
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertVolumes(device);
        }

        private static void AssertVolumes(Device device)
        {
            device.Volumes.Should().NotBeNull();
            device.Volumes.Should().NotContainNulls();
            device.Volumes.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task FetchWindowsScoreFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.WindowsScore
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertWindowsScore(device);
        }

        private static void AssertWindowsScore(Device device)
        {
            device.WindowsScore.Should().NotBeNull();
            device.WindowsScore.Should().NotBeEquivalentTo(new WindowsScore());
        }

        [TestMethod]
        public async Task FetchAllFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.All
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertFetchingDevice(device);
            AssertCompareLocalData(device);
            AssertBIOS(device);
            AssertBaseBoard(device);
            AssertComputerSystem(device);
            AssertDesktops(device);
            AssertDiskDrives(device);
            AssertDiskPartitions(device);
            AssertDisks(device);
            AssertEnvironments(device);
            AssertLocalTime(device);
            AssertNetworkAdapterConfigurations(device);
            AssertNetworkAdapters(device);
            AssertOperatingSystem(device);
            AssertPNPEntities(device);
            AssertPNPSignedDrivers(device);
            AssertPhysicalMemories(device);
            AssertPrinterConfigurations(device);
            AssertPrinters(device);
            AssertProcesses(device);
            AssertProcessor(device);
            AssertProducts(device);
            AssertQuickFixEngineerings(device);
            AssertSerialPortConfigurations(device);
            AssertSerialPorts(device);
            AssertServices(device);
            AssertSoftwareLicensingService(device);
            AssertTimeZone(device);
            AssertVideoControllers(device);
            AssertVolumes(device);
            AssertWindowsScore(device);
        }

        [TestMethod]
        public async Task CompareDataFromLocalhostAsync()
        {
            var searchInformationTypes = new[]
            {
                InformationType.LocalTime,
                InformationType.Desktops
            };

            var device = await FetchDeviceAsync(searchInformationTypes);

            AssertCompareLocalData(device);
        }

        private static void AssertCompareLocalData(Device device)
        {
            device.LocalTime.Year.ToString().Should().Be(DateTime.Now.Year.ToString());
            device.LocalTime.Month.ToString().Should().Be(DateTime.Now.Month.ToString());
            var isParsed = Enum.TryParse(typeof(DayOfWeek), device.LocalTime.DayOfWeek.ToString(), true, out var dayOfWeek);
            isParsed.Should().BeTrue();
            dayOfWeek.Should().Be(DateTime.Now.DayOfWeek);
            device.LocalTime.Day.ToString().Should().Be(DateTime.Now.Day.ToString());
            device.LocalTime.Hour.ToString().Should().Be(DateTime.Now.Hour.ToString());
            device.Desktops.Any(x => x.Name.Contains(System.Environment.UserName)).Should().BeTrue();
        }

        private static void AssertFetchingDevice(Device device)
        {
            device.EndPoint.Should().NotBeNull();
            device.EndPoint.Should().Be(System.Environment.MachineName);
            device.ExecutionTime.Should().BeLessThan(TimeSpan.FromHours(1));
        }

        private async Task<Device> FetchDeviceAsync(InformationType[] searchInformationTypes)
        {
            var localAccessService = new WMIAccessService();
            var connected = await localAccessService.TryConnectAsync().ConfigureAwait(false);
            connected.Should().BeTrue();
            var device = await new Device()
                .WithWMIAccessService(localAccessService)
                .WithInformationTypes(searchInformationTypes)
                .InitializeAsync()
                .ConfigureAwait(false);
            _device = device;
            return device;
        }
    }
}
