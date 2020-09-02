using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("CIM_DataFile")]
    public class DataFile
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string InstallDate { get; set; }
        public string Status { get; set; }
        public uint AccessMask { get; set; }
        public bool Archive { get; set; }
        public bool Compressed { get; set; }
        public string CompressionMethod { get; set; }
        public string CreationClassName { get; set; }
        public string CreationDate { get; set; }
        public string CSCreationClassName { get; set; }
        public string CSName { get; set; }
        public string Drive { get; set; }
        public string EightDotThreeFileName { get; set; }
        public bool Encrypted { get; set; }
        public string EncryptionMethod { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public ulong FileSize { get; set; }
        public string FileType { get; set; }
        public string FSCreationClassName { get; set; }
        public string FSName { get; set; }
        public bool Hidden { get; set; }
        public ulong InUseCount { get; set; }
        public string LastAccessed { get; set; }
        public string LastModified { get; set; }
        public string Path { get; set; }
        public bool Readable { get; set; }
        public bool System { get; set; }
        public bool Writeable { get; set; }
        public string Manufacturer { get; set; }
        public string Version { get; set; }

        public override string ToString()
        {
            return Name + " " + Version;
        }
    }
}
