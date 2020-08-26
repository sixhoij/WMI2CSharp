using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_SerialPortConfiguration")]
    public class SerialPortConfiguration
    {
        public bool AbortReadWriteOnError { get; set; }
        public uint BaudRate { get; set; }
        public bool BinaryModeEnabled { get; set; }
        public uint BitsPerByte { get; set; }
        public string Caption { get; set; }
        public bool ContinueXMitOnXOff { get; set; }
        public bool CTSOutflowControl { get; set; }
        public string Description { get; set; }
        public bool DiscardNULLBytes { get; set; }
        public bool DSROutflowControl { get; set; }
        public bool DSRSensitivity { get; set; }
        public string DTRFlowControlType { get; set; }
        public uint EOFCharacter { get; set; }
        public uint ErrorReplaceCharacter { get; set; }
        public bool ErrorReplacementEnabled { get; set; }
        public uint EventCharacter { get; set; }
        public bool IsBusy { get; set; }
        public string Name { get; set; }
        public string Parity { get; set; }
        public bool ParityCheckEnabled { get; set; }
        public string RTSFlowControlType { get; set; }
        public string SettingID { get; set; }
        public string StopBits { get; set; }
        public uint XOffCharacter { get; set; }
        public uint XOffXMitThreshold { get; set; }
        public uint XOnCharacter { get; set; }
        public uint XOnXMitThreshold { get; set; }
        public uint XOnXOffInFlowControl { get; set; }
        public uint XOnXOffOutFlowControl { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
