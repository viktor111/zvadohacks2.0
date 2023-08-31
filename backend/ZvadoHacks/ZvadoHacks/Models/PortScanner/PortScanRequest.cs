using ZvadoHacks.Models.Types;

namespace ZvadoHacks.Models.PortScanner
{
    public class PortScanRequest
    {
        public string Host { get; set; }

        public PortScanType PortScanType { get; set; }
    }
}
