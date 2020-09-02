using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_DiskPartition")]
    public class DiskPartition
    {
        public string Access { get; set; }
        public string Availability { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public string[] IdentifyingDescriptions { get; set; }
        public ulong BlockSize { get; set; }
        public bool Bootable { get; set; }
        public bool BootPartition { get; set; }
        public string Caption { get; set; }
        public string ConfigManagerErrorCode { get; set; }
        public string ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public uint DiskIndex { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMethodology { get; set; }
        public string HiddenSectors { get; set; }
        public uint Index { get; set; }
        public string InstallDate { get; set; }
        public string LastErrorCode { get; set; }
        public string Name { get; set; }
        public ulong NumberOfBlocks { get; set; }
        public string PNPDeviceID { get; set; }
        public string PowerManagementSupported { get; set; }
        public bool PrimaryPartition { get; set; }
        public string Purpose { get; set; }
        public string RewritePartition { get; set; }
        public ulong Size { get; set; }
        public ulong StartingOffset { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
