using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_PnPEntity")]
    public class PNPEntity
    {
        public string Availability { get; set; }
        public string Caption { get; set; }
        public string ClassGuid { get; set; }
        public string[] CompatibleID { get; set; }
        public uint ConfigManagerErrorCode { get; set; }
        public bool ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string[] HardwareID { get; set; }
        public string InstallDate { get; set; }
        public string LastErrorCode { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string PNPClass { get; set; }
        public string PNPDeviceID { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public string PowerManagementSupported { get; set; }
        public bool Present { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
