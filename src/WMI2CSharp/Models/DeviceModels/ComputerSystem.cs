using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_ComputerSystem")]
    public class ComputerSystem
    {
        public ushort AdminPasswordStatus { get; set; }
        public bool AutomaticManagedPagefile { get; set; }
        public bool AutomaticResetBootOption { get; set; }
        public bool AutomaticResetCapability { get; set; }
        public string BootOptionOnLimit { get; set; }
        public string BootOptionOnWatchDog { get; set; }
        public bool BootROMSupported { get; set; }
        public string BootupState { get; set; }
        public ushort[] BootStatus { get; set; }
        public string Caption { get; set; }
        public ushort ChassisBootupState { get; set; }
        public string ChassisSKUNumber { get; set; }
        public string CreationClassName { get; set; }
        public int CurrentTimeZone { get; set; }
        public bool DaylightInEffect { get; set; }
        public string Description { get; set; }
        public string DNSHostName { get; set; }
        public string Domain { get; set; }
        public ushort DomainRole { get; set; }
        public bool EnableDaylightSavingsTime { get; set; }
        public ushort FrontPanelResetStatus { get; set; }
        public bool HypervisorPresent { get; set; }
        public bool InfraredSupported { get; set; }
        public string[] InitialLoadInfo { get; set; }
        public string InstallDate { get; set; }
        public ushort KeyboardPasswordStatus { get; set; }
        public string LastLoadInfo { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string NameFormat { get; set; }
        public bool NetworkServerModeEnabled { get; set; }
        public ulong NumberOfLogicalProcessors { get; set; }
        public ulong NumberOfProcessors { get; set; }
        public byte[] OEMLogoBitmap { get; set; }
        public string[] OEMStringArray { get; set; }
        public bool PartOfDomain { get; set; }
        public long PauseAfterReset { get; set; }
        public ushort PCSystemType { get; set; }
        public ushort PCSystemTypeEx { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public string PowerManagementSupported { get; set; }
        public ushort PowerOnPasswordStatus { get; set; }
        public ushort PowerState { get; set; }
        public ushort PowerSupplyState { get; set; }
        public string PrimaryOwnerContact { get; set; }
        public string PrimaryOwnerName { get; set; }
        public ushort ResetCapability { get; set; }
        public int ResetCount { get; set; }
        public int ResetLimit { get; set; }
        public string Status { get; set; }
        public string[] SupportContactDescription { get; set; }
        public string SystemFamily { get; set; }
        public string SystemSKUNumber { get; set; }
        public ushort SystemStartupDelay { get; set; }
        public string[] SystemStartupOptions { get; set; }
        public byte SystemStartupSetting { get; set; }
        public string SystemType { get; set; }
        public ushort ThermalState { get; set; }
        public ulong TotalPhysicalMemory { get; set; }
        public string UserName { get; set; }
        public ushort WakeUpType { get; set; }
        public string Workgroup { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
