using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_Printer")]
    public class Printer
    {
        public uint Attributes { get; set; }
        public string Availability { get; set; }
        public string[] AvailableJobSheets { get; set; }
        public uint AveragePagesPerMinute { get; set; }
        public ushort[] Capabilities { get; set; }
        public string[] CapabilityDescriptions { get; set; }
        public string Caption { get; set; }
        public string[] CharSetsSupported { get; set; }
        public string Comment { get; set; }
        public string ConfigManagerErrorCode { get; set; }
        public string ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public ushort[] CurrentCapabilities { get; set; }
        public string CurrentCharSet { get; set; }
        public string CurrentLanguage { get; set; }
        public string CurrentMimeType { get; set; }
        public string CurrentNaturalLanguage { get; set; }
        public string CurrentPaperType { get; set; }
        public bool Default { get; set; }
        public ushort[] DefaultCapabilities { get; set; }
        public string DefaultCopies { get; set; }
        public string DefaultLanguage { get; set; }
        public string DefaultMimeType { get; set; }
        public string DefaultNumberUp { get; set; }
        public string DefaultPaperType { get; set; }
        public uint DefaultPriority { get; set; }
        public string Description { get; set; }
        public ushort DetectedErrorState { get; set; }
        public string DeviceID { get; set; }
        public bool Direct { get; set; }
        public bool DoCompleteFirst { get; set; }
        public string DriverName { get; set; }
        public bool EnableBIDI { get; set; }
        public bool EnableDevQueryPrint { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string[] ErrorInformation { get; set; }
        public ushort ExtendedDetectedErrorState { get; set; }
        public ushort ExtendedPrinterStatus { get; set; }
        public bool Hidden { get; set; }
        public uint HorizontalResolution { get; set; }
        public string InstallDate { get; set; }
        public uint JobCountSinceLastReset { get; set; }
        public bool KeepPrintedJobs { get; set; }
        public ushort[] LanguagesSupported { get; set; }
        public string LastErrorCode { get; set; }
        public bool Local { get; set; }
        public string Location { get; set; }
        public string MarkingTechnology { get; set; }
        public string MaxCopies { get; set; }
        public string MaxNumberUp { get; set; }
        public string MaxSizeSupported { get; set; }
        public string[] MimeTypesSupported { get; set; }
        public string Name { get; set; }
        public string[] NaturalLanguagesSupported { get; set; }
        public bool Network { get; set; }
        public ushort[] PaperSizesSupported { get; set; }
        public string[] PaperTypesAvailable { get; set; }
        public string Parameters { get; set; }
        public string PNPDeviceID { get; set; }
        public string PortName { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public bool PowerManagementSupported { get; set; }
        public string[] PrinterPaperNames { get; set; }
        public uint PrinterState { get; set; }
        public ushort PrinterStatus { get; set; }
        public string PrintJobDataType { get; set; }
        public string PrintProcessor { get; set; }
        public uint Priority { get; set; }
        public bool Published { get; set; }
        public bool Queued { get; set; }
        public bool RawOnly { get; set; }
        public string SeparatorFile { get; set; }
        public string ServerName { get; set; }
        public bool Shared { get; set; }
        public string ShareName { get; set; }
        public bool SpoolEnabled { get; set; }
        public string StartTime { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public string TimeOfLastReset { get; set; }
        public string UntilTime { get; set; }
        public uint VerticalResolution { get; set; }
        public bool WorkOffline { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
