using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_Desktop")]
    public class Desktop
    {
        public ulong BorderWidth { get; set; }
        public string Caption { get; set; }
        public bool CoolSwitch { get; set; }
        public ulong CursorBlinkRate { get; set; }
        public string Description { get; set; }
        public bool DragFullWindows { get; set; }
        public uint GridGranularity { get; set; }
        public uint IconSpacing { get; set; }
        public string IconTitleFaceName { get; set; }
        public ulong IconTitleSize { get; set; }
        public bool IconTitleWrap { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }
        public bool ScreenSaverActive { get; set; }
        public string ScreenSaverExecutable { get; set; }
        public bool ScreenSaverSecure { get; set; }
        public uint ScreenSaverTimeout { get; set; }
        public string SettingID { get; set; }
        public string Wallpaper { get; set; }
        public bool WallpaperStretched { get; set; }
        public bool WallpaperTiled { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
