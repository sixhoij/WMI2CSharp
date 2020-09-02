using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_DiskDrive")]
    public class DiskDrive
    {
        public string Availability { get; set; }
        public uint BytesPerSector { get; set; }
        public ushort[] Capabilities { get; set; }
        public string[] CapabilityDescriptions { get; set; }
        public string Caption { get; set; }
        public string CompressionMethod { get; set; }
        public uint ConfigManagerErrorCode { get; set; }
        public bool ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string DefaultBlockSize { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMethodology { get; set; }
        public string FirmwareRevision { get; set; }
        public uint Index { get; set; }
        public string InstallDate { get; set; }
        public string InterfaceType { get; set; }
        public string LastErrorCode { get; set; }
        public string Manufacturer { get; set; }
        public string MaxBlockSize { get; set; }
        public string MaxMediaSize { get; set; }
        public bool MediaLoaded { get; set; }
        public string MediaType { get; set; }
        public string MinBlockSize { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string NeedsCleaning { get; set; }
        public string NumberOfMediaSupported { get; set; }
        public uint Partitions { get; set; }
        public string PNPDeviceID { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public string PowerManagementSupported { get; set; }
        public uint SCSIBus { get; set; }
        public ushort SCSILogicalUnit { get; set; }
        public ushort SCSIPort { get; set; }
        public ushort SCSITargetId { get; set; }
        public uint SectorsPerTrack { get; set; }
        public string SerialNumber { get; set; }
        public uint Signature { get; set; }
        public ulong Size { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public ulong TotalCylinders { get; set; }
        public uint TotalHeads { get; set; }
        public ulong TotalSectors { get; set; }
        public ulong TotalTracks { get; set; }
        public uint TracksPerCylinder { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
