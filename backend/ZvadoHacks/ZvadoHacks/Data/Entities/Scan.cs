using ZvadoHacks.Models.PortScanner;

namespace ZvadoHacks.Data.Entities
{
    public class Scan
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public PortScanResponse? PortScanResponse { get; set; }
    }
}
