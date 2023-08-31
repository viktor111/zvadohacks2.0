using ZvadoHacks.Models.Types;

namespace ZvadoHacks.Models.PortScanner
{
    public class PortScanResponse
    {
        public List<Port> Ports { get; set; }

        public int OpenPorts { get; set; }

        public string TimeTaken { get; set; }

        public PortScanType PortScanType { get; set; }

        public List<string> IpsScanned { get; set; }
    }
}
