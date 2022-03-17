using CountwareTraffic.Services.Devices.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class DeviceStatusEntityTypeConfiguration : IEntityTypeConfiguration<DeviceStatus>
    {
        public void Configure(EntityTypeBuilder<DeviceStatus> builder)
        {
            builder.ToTable("DeviceStatuses", SchemaNames.Devices);

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
