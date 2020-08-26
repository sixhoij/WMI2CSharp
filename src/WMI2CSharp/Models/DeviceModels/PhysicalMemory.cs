using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_PhysicalMemory")]
    public class PhysicalMemory
    {
        public uint Attributes { get; set; }
        public string BankLabel { get; set; }
        public ulong Capacity { get; set; }
        public string Caption { get; set; }
        public uint ConfiguredClockSpeed { get; set; }
        public uint ConfiguredVoltage { get; set; }
        public string CreationClassName { get; set; }
        public ushort DataWidth { get; set; }
        public string Description { get; set; }
        public string DeviceLocator { get; set; }
        public ushort FormFactor { get; set; }
        public string HotSwappable { get; set; }
        public string InstallDate { get; set; }
        public ushort InterleaveDataDepth { get; set; }
        public uint InterleavePosition { get; set; }
        public string Manufacturer { get; set; }
        public uint MaxVoltage { get; set; }
        public ushort MemoryType { get; set; }
        public uint MinVoltage { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string OtherIdentifyingInfo { get; set; }
        public string PartNumber { get; set; }
        public uint PositionInRow { get; set; }
        public string PoweredOn { get; set; }
        public string Removable { get; set; }
        public string Replaceable { get; set; }
        public string SerialNumber { get; set; }
        public string SKU { get; set; }
        public uint SMBIOSMemoryType { get; set; }
        public uint Speed { get; set; }
        public string Status { get; set; }
        public string Tag { get; set; }
        public ushort TotalWidth { get; set; }
        public ushort TypeDetail { get; set; }
        public string Version { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
