using ZvadoHacks.Models.PortScanner;

namespace ZvadoHacks.Services
{
    public interface IPortScannerService
    {
        public Task<PortScanResponse> Scan(PortScanRequest request, string userId);
    }
}
