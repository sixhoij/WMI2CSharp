using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_NetworkAdapterConfiguration")]
    public class NetworkAdapterConfiguration
    {
        public string ArpAlwaysSourceRoute { get; set; }
        public string ArpUseEtherSNAP { get; set; }
        public string Caption { get; set; }
        public string DatabasePath { get; set; }
        public bool DeadGWDetectEnabled { get; set; }
        public string DefaultTOS { get; set; }
        public string DefaultTTL { get; set; }
        public string Description { get; set; }
        public bool DHCPEnabled { get; set; }
        public string DHCPLeaseExpires { get; set; }
        public string DHCPLeaseObtained { get; set; }
        public string DHCPServer { get; set; }
        public string DNSDomain { get; set; }
        public bool DNSEnabledForWINSResolution { get; set; }
        public string DNSHostName { get; set; }
        public bool DomainDNSRegistrationEnabled { get; set; }
        public string ForwardBufferMemory { get; set; }
        public bool FullDNSRegistrationEnabled { get; set; }
        public string IGMPLevel { get; set; }
        public uint Index { get; set; }
        public uint InterfaceIndex { get; set; }
        public uint IPConnectionMetric { get; set; }
        public bool IPEnabled { get; set; }
        public bool IPFilterSecurityEnabled { get; set; }
        public bool IPPortSecurityEnabled { get; set; }
        public string IPUseZeroBroadcast { get; set; }
        public string IPXAddress { get; set; }
        public bool IPXEnabled { get; set; }
        public string IPXMediaType { get; set; }
        public string IPXVirtualNetNumber { get; set; }
        public string KeepAliveInterval { get; set; }
        public string KeepAliveTime { get; set; }
        public string MACAddress { get; set; }
        public string MTU { get; set; }
        public string NumForwardPackets { get; set; }
        public bool PMTUBHDetectEnabled { get; set; }
        public bool PMTUDiscoveryEnabled { get; set; }
        public string ServiceName { get; set; }
        public string SettingID { get; set; }
        public uint TcpipNetbiosOptions { get; set; }
        public string TcpMaxConnectRetransmissions { get; set; }
        public string TcpMaxDataRetransmissions { get; set; }
        public string TcpNumConnections { get; set; }
        public string TcpUseRFC1122UrgentPointer { get; set; }
        public ushort TcpWindowSize { get; set; }
        public bool WINSEnableLMHostsLookup { get; set; }
        public string WINSHostLookupFile { get; set; }
        public string WINSPrimaryServer { get; set; }
        public string WINSScopeID { get; set; }
        public string WINSSecondaryServer { get; set; }
    }
}
