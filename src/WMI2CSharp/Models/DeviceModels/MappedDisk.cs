using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_MappedLogicalDisk")]
    public class MappedDisk
    {
        public ushort Access { get; set; }
        public ushort Availability { get; set; }
        public ulong BlockSize { get; set; }
        public string Caption { get; set; }
        public bool Compressed { get; set; }
        public uint ConfigManagerErrorCode { get; set; }
        public bool ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public bool ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMethodology { get; set; }
        public string FileSystem { get; set; }
        public ulong FreeSpace { get; set; }
        public string InstallDate { get; set; }
        public uint LastErrorCode { get; set; }
        public uint MaximumComponentLength { get; set; }
        public string Name { get; set; }
        public ulong NumberOfBlocks { get; set; }
        public string PNPDeviceID { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public bool PowerManagementSupported { get; set; }
        public string ProviderName { get; set; }
        public string Purpose { get; set; }
        public bool QuotasDisabled { get; set; }
        public bool QuotasIncomplete { get; set; }
        public bool QuotasRebuilding { get; set; }
        public string SessionID { get; set; }
        public ulong Size { get; set; }
        public string Status { get; set; }
        public ushort StatusInfo { get; set; }
        public bool SupportsDiskQuotas { get; set; }
        public bool SupportsFileBasedCompression { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public string VolumeName { get; set; }
        public string VolumeSerialNumber { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
