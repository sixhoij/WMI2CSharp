# WMI2CSharp

## Description
WMI2CSharp is a library for Windows Management Instrumentation Object Relational Mapping. <br>
It will fetch data upon provided access information and hostname. <br>
Task optimized and parameterizable to minimize fetch time. <br>

# Sample Usage
## Simple
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

## Advanced
The cancellation token will cancel all incomplete tasks, that means the result from GetDevicesAsync only returns finished devices that have completed all the data fetching.

    var cancellationTokenSource = new CancellationTokenSource();
    cancellationTokenSource.CancelAfter(new TimeSpan(0, 1, 0));
    var cancellationToken = cancellationTokenSource.Token;
    
    var dataFilesPath = @"C:\\Path\\";
    var hostNames = new[] { "HostName1", "HostName2", "HostName3" };
    var deviceCollection = new DeviceCollection();
    var devices = await deviceCollection
                        .WithCredentials(new CredentialsProvider("UserName","Password"))
                        .Where(InformationType.DataFiles, $"{nameof(DataFile.Name)} = '{dataFilesPath}ApplicationName1.exe'")
                        .Where(InformationType.DataFiles, $"{nameof(DataFile.Name)} = '{dataFilesPath}ApplicationName2.exe'")
                        .Where(InformationType.DataFiles, $"where {nameof(DataFile.Name)} = '{dataFilesPath}ApplicationName3.exe'")
                        .Where(InformationType.DataFiles, $"where {nameof(DataFile.Name)} = '{dataFilesPath}subpath\\ApplicationName1.exe'")
                        .Where(InformationType.DataFiles, $"{nameof(DataFile.Name)} = '{dataFilesPath}subpath\\ApplicationName2.exe'")
                        .Where(InformationType.DataFiles, $"{nameof(DataFile.Name)} = '{dataFilesPath}subpath\\Library.dll'")                
                        .Where(InformationType.DataFiles, $@"{nameof(DataFile.Path)} = '\\Program Files (x86)\\Notepad++\\'")                
                        .Where(InformationType.PNPEntities, $"{nameof(PNPEntity.Description)} LIKE '% PCI %'")
                        .GetDevicesAsync(hostNames, cancellationToken)
                        .ConfigureAwait(false);
                        
    var performanceSummary = deviceCollection.Summarize();

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
    PNP,
    DataFiles

## Information Types
    All,
    BaseBoard,
    BIOS,
    ComputerSystem,
    DataFiles,
    Desktops,
    Disks,
    DiskDrives,
    DiskPartitions,
    Environments,
    LocalTime,
    NetworkAdapters,
    NetworkAdapterConfigurations,
    OperatingSystem,
    PhysicalMemories,
    PNPEntities,
    PNPSignedDrivers,
    Printers,
    PrinterConfigurations,
    Processes,
    Processor,
    Products,
    QuickFixEngineerings,
    SerialPorts,
    SerialPortConfigurations,
    Services,
    SoftwareLicensingService,
    TimeZone,
    VideoControllers,
    Volumes,
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
        public static event EventHandler<EventArgs<WMIGeneralException>> OnExceptionMessage;
        public static event EventHandler<EventArgs<OperationCanceledException>> OnTaskIncompleted;


<p>OnConnectionMessage will publish messages about connection on hosts.</p>
<p>OnInformationMessage will publish messages about fetched properties from hosts.</p>
<p>OnWarningMessage will publish messages about warnings.</p>
<p>OnExceptionMessage will publish WMI exceptions.</p>
<p>OnTaskIncompleted will publish task cancelled exceptions.</p>
<p>OnDebugMessage not yet implemented.</p>
<p>OnErrorMessage not yet implemented.</p>

**Sample Usage:**

    LogEventHandler.OnInformationMessage += LogEventHandler_OnInformationMessage;
    private static void LogEventHandler_OnInformationMessage(object sender, EventArgs<string> eventArgs)
    {
        Console.WriteLine(eventArgs.Object);
    }
