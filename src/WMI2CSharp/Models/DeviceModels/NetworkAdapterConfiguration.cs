using WMI2CSharp.Attributes;

namespace WMI2CSharp.Models.DeviceModels
{
    [WMIClass("Win32_NetworkAdapterConfiguration")]
    public class NetworkAdapterConfiguration
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string SettingID { get; set; }
        public bool ArpAlwaysSourceRoute { get; set; }
        public bool ArpUseEtherSNAP { get; set; }
        public string DatabasePath { get; set; }
        public bool DeadGWDetectEnabled { get; set; }
        public string[] DefaultIPGateway { get; set; }
        public byte DefaultTOS { get; set; }
        public byte DefaultTTL { get; set; }
        public bool DHCPEnabled { get; set; }
        public string DHCPLeaseExpires { get; set; }
        public string DHCPLeaseObtained { get; set; }
        public string DHCPServer { get; set; }
        public string DNSDomain { get; set; }
        public string[] DNSDomainSuffixSearchOrder { get; set; }
        public bool DNSEnabledForWINSResolution { get; set; }
        public string DNSHostName { get; set; }
        public string[] DNSServerSearchOrder { get; set; }
        public bool DomainDNSRegistrationEnabled { get; set; }
        public uint ForwardBufferMemory { get; set; }
        public bool FullDNSRegistrationEnabled { get; set; }
        public ushort[] GatewayCostMetric { get; set; }
        public byte IGMPLevel { get; set; }
        public uint Index { get; set; }
        public uint InterfaceIndex { get; set; }
        public string[] IPAddress { get; set; }
        public uint IPConnectionMetric { get; set; }
        public bool IPEnabled { get; set; }
        public bool IPFilterSecurityEnabled { get; set; }
        public bool IPPortSecurityEnabled { get; set; }
        public string[] IPSecPermitIPProtocols { get; set; }
        public string[] IPSecPermitTCPPorts { get; set; }
        public string[] IPSecPermitUDPPorts { get; set; }
        public string[] IPSubnet { get; set; }
        public bool IPUseZeroBroadcast { get; set; }
        public string IPXAddress { get; set; }
        public bool IPXEnabled { get; set; }
        public uint[] IPXFrameType { get; set; }
        public uint IPXMediaType { get; set; }
        public string[] IPXNetworkNumber { get; set; }
        public string IPXVirtualNetNumber { get; set; }
        public uint KeepAliveInterval { get; set; }
        public uint KeepAliveTime { get; set; }
        public string MACAddress { get; set; }
        public uint MTU { get; set; }
        public uint NumForwardPackets { get; set; }
        public bool PMTUBHDetectEnabled { get; set; }
        public bool PMTUDiscoveryEnabled { get; set; }
        public string ServiceName { get; set; }
        public uint TcpipNetbiosOptions { get; set; }
        public uint TcpMaxConnectRetransmissions { get; set; }
        public uint TcpMaxDataRetransmissions { get; set; }
        public uint TcpNumConnections { get; set; }
        public bool TcpUseRFC1122UrgentPointer { get; set; }
        public ushort TcpWindowSize { get; set; }
        public bool WINSEnableLMHostsLookup { get; set; }
        public string WINSHostLookupFile { get; set; }
        public string WINSPrimaryServer { get; set; }
        public string WINSScopeID { get; set; }
        public string WINSSecondaryServer { get; set; }

        public override string ToString()
        {
            return Caption;
        }
    }
}
