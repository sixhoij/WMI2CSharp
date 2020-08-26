using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_Environment")]
    public class Environment
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string InstallDate { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool SystemVariable { get; set; }
        public string UserName { get; set; }
        public string VariableValue { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
