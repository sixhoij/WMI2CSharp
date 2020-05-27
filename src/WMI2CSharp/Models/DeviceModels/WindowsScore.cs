using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_WinSAT")]
    public class WindowsScore
    {
        public float CPUScore { get; set; }
        public float D3DScore { get; set; }
        public float DiskScore { get; set; }
        public float GraphicsScore { get; set; }
        public float MemoryScore { get; set; }
        public string TimeTaken { get; set; }
        public uint WinSATAssessmentState { get; set; }
        public float WinSPRLevel { get; set; }
    }
}
