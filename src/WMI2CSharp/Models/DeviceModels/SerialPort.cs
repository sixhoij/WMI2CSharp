using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_SerialPort")]
    public class SerialPort
    {
        public ushort Availability { get; set; }
        public bool Binary { get; set; }
        public ushort[] Capabilities { get; set; }
        public string[] CapabilityDescriptions { get; set; }
        public string Caption { get; set; }
        public uint ConfigManagerErrorCode { get; set; }
        public bool ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string InstallDate { get; set; }
        public string LastErrorCode { get; set; }
        public uint MaxBaudRate { get; set; }
        public uint MaximumInputBufferSize { get; set; }
        public uint MaximumOutputBufferSize { get; set; }
        public string MaxNumberControlled { get; set; }
        public string Name { get; set; }
        public bool OSAutoDiscovered { get; set; }
        public string PNPDeviceID { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public bool PowerManagementSupported { get; set; }
        public string ProtocolSupported { get; set; }
        public string ProviderType { get; set; }
        public bool SettableBaudRate { get; set; }
        public bool SettableDataBits { get; set; }
        public bool SettableFlowControl { get; set; }
        public bool SettableParity { get; set; }
        public bool SettableParityCheck { get; set; }
        public bool SettableRLSD { get; set; }
        public bool SettableStopBits { get; set; }
        public string Status { get; set; }
        public ushort StatusInfo { get; set; }
        public bool Supports16BitMode { get; set; }
        public bool SupportsDTRDSR { get; set; }
        public bool SupportsElapsedTimeouts { get; set; }
        public bool SupportsIntTimeouts { get; set; }
        public bool SupportsParityCheck { get; set; }
        public bool SupportsRLSD { get; set; }
        public bool SupportsRTSCTS { get; set; }
        public bool SupportsSpecialCharacters { get; set; }
        public bool SupportsXOnXOff { get; set; }
        public bool SupportsXOnXOffSet { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public string TimeOfLastReset { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
