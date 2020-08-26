using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_LocalTime")]
    public class LocalTime
    {
        public uint Day { get; set; }
        public uint DayOfWeek { get; set; }
        public uint Hour { get; set; }
        public uint Milliseconds { get; set; }
        public uint Minute { get; set; }
        public uint Month { get; set; }
        public uint Quarter { get; set; }
        public uint Second { get; set; }
        public uint WeekInMonth { get; set; }
        public uint Year { get; set; }

        public override string ToString()
        {
            return Year + "/" + Month + "/" + Day;
        }
    }
}
