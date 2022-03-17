using CountwareTraffic.Services.Devices.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class DeviceTypeEntityTypeConfiguration : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.ToTable("DeviceTypes", SchemaNames.Devices);

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
