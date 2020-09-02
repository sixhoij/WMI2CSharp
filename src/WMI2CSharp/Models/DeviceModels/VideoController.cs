using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_VideoController")]
    public class VideoController
    {
        public ushort[] AcceleratorCapabilities { get; set; }
        public string AdapterCompatibility { get; set; }
        public string AdapterDACType { get; set; }
        public uint AdapterRAM { get; set; }
        public ushort Availability { get; set; }
        public string[] CapabilityDescriptions { get; set; }
        public string Caption { get; set; }
        public string ColorTableEntries { get; set; }
        public uint ConfigManagerErrorCode { get; set; }
        public bool ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public uint CurrentBitsPerPixel { get; set; }
        public uint CurrentHorizontalResolution { get; set; }
        public ulong CurrentNumberOfColors { get; set; }
        public uint CurrentNumberOfColumns { get; set; }
        public uint CurrentNumberOfRows { get; set; }
        public uint CurrentRefreshRate { get; set; }
        public ushort CurrentScanMode { get; set; }
        public uint CurrentVerticalResolution { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public uint DeviceSpecificPens { get; set; }
        public uint DitherType { get; set; }
        public string DriverDate { get; set; }
        public string DriverVersion { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string ICMIntent { get; set; }
        public string ICMMethod { get; set; }
        public string InfFilename { get; set; }
        public string InfSection { get; set; }
        public string InstallDate { get; set; }
        public string InstalledDisplayDrivers { get; set; }
        public string LastErrorCode { get; set; }
        public string MaxMemorySupported { get; set; }
        public string MaxNumberControlled { get; set; }
        public uint MaxRefreshRate { get; set; }
        public uint MinRefreshRate { get; set; }
        public bool Monochrome { get; set; }
        public string Name { get; set; }
        public ushort NumberOfColorPlanes { get; set; }
        public string NumberOfVideoPages { get; set; }
        public string PNPDeviceID { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public string PowerManagementSupported { get; set; }
        public string ProtocolSupported { get; set; }
        public string ReservedSystemPaletteEntries { get; set; }
        public string SpecificationVersion { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public string SystemPaletteEntries { get; set; }
        public string TimeOfLastReset { get; set; }
        public ushort VideoArchitecture { get; set; }
        public ushort VideoMemoryType { get; set; }
        public string VideoMode { get; set; }
        public string VideoModeDescription { get; set; }
        public string VideoProcessor { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
