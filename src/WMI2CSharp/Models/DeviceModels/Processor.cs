﻿using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_Processor")]
    public class Processor
    {
        public ushort AddressWidth { get; set; }
        public ushort Architecture { get; set; }
        public string AssetTag { get; set; }
        public ushort Availability { get; set; }
        public string Caption { get; set; }
        public uint Characteristics { get; set; }
        public string ConfigManagerErrorCode { get; set; }
        public string ConfigManagerUserConfig { get; set; }
        public ushort CpuStatus { get; set; }
        public string CreationClassName { get; set; }
        public uint CurrentClockSpeed { get; set; }
        public ushort CurrentVoltage { get; set; }
        public ushort DataWidth { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public string ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public uint ExtClock { get; set; }
        public ushort Family { get; set; }
        public string InstallDate { get; set; }
        public uint L2CacheSize { get; set; }
        public uint L2CacheSpeed { get; set; }
        public uint L3CacheSize { get; set; }
        public uint L3CacheSpeed { get; set; }
        public string LastErrorCode { get; set; }
        public ushort Level { get; set; }
        public ushort LoadPercentage { get; set; }
        public string Manufacturer { get; set; }
        public uint MaxClockSpeed { get; set; }
        public string Name { get; set; }
        public uint NumberOfCores { get; set; }
        public uint NumberOfEnabledCore { get; set; }
        public uint NumberOfLogicalProcessors { get; set; }
        public string OtherFamilyDescription { get; set; }
        public string PartNumber { get; set; }
        public string PNPDeviceID { get; set; }
        public bool PowerManagementSupported { get; set; }
        public string ProcessorId { get; set; }
        public ushort ProcessorType { get; set; }
        public ushort Revision { get; set; }
        public string Role { get; set; }
        public bool SecondLevelAddressTranslationExtensions { get; set; }
        public string SerialNumber { get; set; }
        public string SocketDesignation { get; set; }
        public string Status { get; set; }
        public ushort StatusInfo { get; set; }
        public string Stepping { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public uint ThreadCount { get; set; }
        public string UniqueId { get; set; }
        public ushort UpgradeMethod { get; set; }
        public string Version { get; set; }
        public bool VirtualizationFirmwareEnabled { get; set; }
        public bool VMMonitorModeExtensions { get; set; }
        public string VoltageCaps { get; set; }

        public override string ToString()
        {
            return Name + " " + Caption;
        }
    }
}
