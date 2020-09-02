using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_LogicalDisk")]
    public class Disk
    {
        public ushort Access { get; set; }
        public string Availability { get; set; }
        public string BlockSize { get; set; }
        public string Caption { get; set; }
        public bool Compressed { get; set; }
        public string ConfigManagerErrorCode { get; set; }
        public string ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public uint DriveType { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMethodology { get; set; }
        public string FileSystem { get; set; }
        public ulong FreeSpace { get; set; }
        public string InstallDate { get; set; }
        public string LastErrorCode { get; set; }
        public uint MaximumComponentLength { get; set; }
        public uint MediaType { get; set; }
        public string Name { get; set; }
        public string NumberOfBlocks { get; set; }
        public string PNPDeviceID { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public string PowerManagementSupported { get; set; }
        public string ProviderName { get; set; }
        public string Purpose { get; set; }
        public bool QuotasDisabled { get; set; }
        public bool QuotasIncomplete { get; set; }
        public bool QuotasRebuilding { get; set; }
        public ulong Size { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public bool SupportsDiskQuotas { get; set; }
        public bool SupportsFileBasedCompression { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public bool VolumeDirty { get; set; }
        public string VolumeName { get; set; }
        public string VolumeSerialNumber { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
