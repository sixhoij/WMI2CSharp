using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_TimeZone")]
    public class TimeZone
    {
        public int Bias { get; set; }
        public string Caption { get; set; }
        public int DaylightBias { get; set; }
        public uint DaylightDay { get; set; }
        public byte DaylightDayOfWeek { get; set; }
        public uint DaylightHour { get; set; }
        public uint DaylightMillisecond { get; set; }
        public uint DaylightMinute { get; set; }
        public uint DaylightMonth { get; set; }
        public string DaylightName { get; set; }
        public uint DaylightSecond { get; set; }
        public uint DaylightYear { get; set; }
        public string Description { get; set; }
        public string SettingID { get; set; }
        public uint StandardBias { get; set; }
        public uint StandardDay { get; set; }
        public byte StandardDayOfWeek { get; set; }
        public uint StandardHour { get; set; }
        public uint StandardMillisecond { get; set; }
        public uint StandardMinute { get; set; }
        public uint StandardMonth { get; set; }
        public string StandardName { get; set; }
        public uint StandardSecond { get; set; }
        public uint StandardYear { get; set; }

        public override string ToString()
        {
            return Caption;
        }
    }
}
