# WMI2CSharp

## Description
WMI2CSharp is a library for Windows Management Instrumentation Object Relational Mapping. <br>
It will fetch data upon provided access information and hostname. <br>
Task optimized and parameterizable to minimize fetch time. <br>

## Sample Usage
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
        InformationType.SerialPort
    };

    var hostNames = new[] { "HostName1", "HostName2", "HostName3" };
    var devices = new List<Device>(await new DeviceCollection()
                                            .WithCredentials(new CredentialsProvider("UserName","Password"))
                                            .WithInformationCategories(searchInformationCategories)
                                            .WithInformationTypes(searchInformationTypes)
                                            .GetDevicesAsync(hostNames)
                                            .ConfigureAwait(false));

## Information Categories
    All,
    Software,
    InternalHardware,
    OperatingSystem,
    ExternalHardware,
    System,
    User,
    Application,
    Network,
    Configuration,
    PNP

## Information Types
    All,
    BaseBoard,
    BIOS,
    ComputerSystem,
    Desktop,
    Disk,
    DiskDrive,
    DiskPartition,
    Environment,
    LocalTime,
    NetworkAdapter,
    NetworkAdapterConfiguration,
    OperatingSystem,
    PhysicalMemory,
    PNPEntity,
    PNPSignedDriver,
    Printer,
    PrinterConfiguration,
    Process,
    Processor,
    Product,
    QuickFixEngineering,
    SerialPort,
    SerialPortConfiguration,
    Service,
    TimeZone,
    VideoController,
    Volume,
    WindowsScore

## Credential Provider
This is an inbuilt CredentialsProvider. <br>
This can be customized to have anything provide the credential information as long as it implements the interface "ICredentialsProvider".

    public class CredentialsProvider : ICredentialsProvider
    {
        private readonly string _user;
        private readonly string _pass;
        private readonly string _domain;

        public CredentialsProvider(string user, string pass, string domain = null)
        {
            _user = user;
            _pass = pass;
            _domain = domain;
        }

        public async Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName)
        {
            return await Task.Run(() => new WMIConnectionOption(hostName, _domain, _user, _pass)).ConfigureAwait(false);
        }

        public async Task<WMIConnectionOption> GetWMIConnectionOptionAsync(string hostName, string domain)
        {
            return await Task.Run(() => new WMIConnectionOption(hostName, domain, _user, _pass)).ConfigureAwait(false);
        }
    }

## Logging
To make it possible to catch log messages asynchronously this will be handled through events.<br>
Available events to subscribe for logging: 

        public static event EventHandler<EventArgs<string>> OnConnectionMessage;
        public static event EventHandler<EventArgs<string>> OnDebugMessage;
        public static event EventHandler<EventArgs<string>> OnInformationMessage;
        public static event EventHandler<EventArgs<string>> OnWarningMessage;
        public static event EventHandler<EventArgs<string>> OnErrorMessage;
        public static event EventHandler<EventArgs<Exception>> OnExceptionMessage;


<p>OnConnectionMessage will publish messages about connection on hosts.</p>
<p>OnInformationMessage will publish messages about fetched properties from hosts.</p>
<p>OnWarningMessage will publish messages about warnings.</p>
<p>OnExceptionMessage will publish exceptions.</p>
<p>OnDebugMessage not yet implemented.</p>
<p>OnErrorMessage not yet implemented.</p>

**Sample Usage:**

    LogEventHandler.OnInformationMessage += LogEventHandler_OnInformationMessage;
    private static void LogEventHandler_OnInformationMessage(object sender, EventArgs<string> eventArgs)
    {
        Console.WriteLine(eventArgs.Object);
    }
