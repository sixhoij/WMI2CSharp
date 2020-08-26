using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("SoftwareLicensingService")]
    public class SoftwareLicensingService
    {
        public string Version { get; set; }
        public string KeyManagementServiceMachine { get; set; }
        public uint IsKeyManagementServiceMachine { get; set; }
        public uint VLActivationInterval { get; set; }
        public uint VLRenewalInterval { get; set; }
        public uint KeyManagementServiceCurrentCount { get; set; }
        public uint RequiredClientCount { get; set; }
        public string KeyManagementServiceProductKeyID { get; set; }
        public string DiscoveredKeyManagementServiceMachineName { get; set; }
        public uint DiscoveredKeyManagementServiceMachinePort { get; set; }
        public uint PolicyCacheRefreshRequired { get; set; }
        public string ClientMachineID { get; set; }
        public uint RemainingWindowsReArmCount { get; set; }
        public uint KeyManagementServiceListeningPort { get; set; }
        public bool KeyManagementServiceDnsPublishing { get; set; }
        public bool KeyManagementServiceLowPriority { get; set; }
        public bool KeyManagementServiceHostCaching { get; set; }
        public uint KeyManagementServiceUnlicensedRequests { get; set; }
        public uint KeyManagementServiceLicensedRequests { get; set; }
        public uint KeyManagementServiceOOBGraceRequests { get; set; }
        public uint KeyManagementServiceOOTGraceRequests { get; set; }
        public uint KeyManagementServiceNonGenuineGraceRequests { get; set; }
        public uint KeyManagementServiceTotalRequests { get; set; }
        public uint KeyManagementServiceFailedRequests { get; set; }
        public uint KeyManagementServiceNotificationRequests { get; set; }
        public string TokenActivationILID { get; set; }
        public uint TokenActivationILVID { get; set; }
        public uint TokenActivationGrantNumber { get; set; }
        public string TokenActivationCertificateThumbprint { get; set; }
        public string TokenActivationAdditionalInfo { get; set; }
        public bool KeyManagementServiceActivationDisabled { get; set; }

        public override string ToString()
        {
            return Version;
        }
    }
}
