using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_QuickFixEngineering")]
    public class QuickFixEngineering
    {
        public string Caption { get; set; }
        public string CSName { get; set; }
        public string Description { get; set; }
        public string FixComments { get; set; }
        public string HotFixID { get; set; }
        public string InstallDate { get; set; }
        public string InstalledBy { get; set; }
        public string InstalledOn { get; set; }
        public string Name { get; set; }
        public string ServicePackInEffect { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
