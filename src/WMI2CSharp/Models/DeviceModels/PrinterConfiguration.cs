using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_PrinterConfiguration")]
    public class PrinterConfiguration
    {
        public uint BitsPerPel { get; set; }
        public string Caption { get; set; }
        public bool Collate { get; set; }
        public uint Color { get; set; }
        public uint Copies { get; set; }
        public string Description { get; set; }
        public string DeviceName { get; set; }
        public uint DisplayFlags { get; set; }
        public string DisplayFrequency { get; set; }
        public uint DitherType { get; set; }
        public uint DriverVersion { get; set; }
        public bool Duplex { get; set; }
        public string FormName { get; set; }
        public uint HorizontalResolution { get; set; }
        public uint ICMIntent { get; set; }
        public uint ICMMethod { get; set; }
        public uint LogPixels { get; set; }
        public uint MediaType { get; set; }
        public string Name { get; set; }
        public uint Orientation { get; set; }
        public uint PaperLength { get; set; }
        public string PaperSize { get; set; }
        public uint PaperWidth { get; set; }
        public string PelsHeight { get; set; }
        public string PelsWidth { get; set; }
        public uint PrintQuality { get; set; }
        public uint Scale { get; set; }
        public string SettingID { get; set; }
        public uint SpecificationVersion { get; set; }
        public uint TTOption { get; set; }
        public uint VerticalResolution { get; set; }
        public uint XResolution { get; set; }
        public uint YResolution { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
