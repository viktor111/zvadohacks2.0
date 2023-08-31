using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using ZvadoHacks.Data.Entities;
using ZvadoHacks.Models.PortScanner;

namespace ZvadoHacks.Data.Configurations
{
    public class ScanConfiguration : IEntityTypeConfiguration<Scan>
    {
        public void Configure(EntityTypeBuilder<Scan> builder)
        {
            builder.Property(x => x.PortScanResponse).IsRequired(false);

            builder
            .Property(x => x.PortScanResponse)
            .HasColumnType("json")
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<PortScanResponse>(x),
                new ValueComparer<PortScanResponse>((d1, d2) => d1.Equals(d2),
                    d => d.GetHashCode(),
                    d => d));
        }
    }
}
