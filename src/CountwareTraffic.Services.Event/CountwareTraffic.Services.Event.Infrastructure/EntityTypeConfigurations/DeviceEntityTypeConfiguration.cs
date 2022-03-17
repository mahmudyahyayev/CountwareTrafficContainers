using CountwareTraffic.Services.Events.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class DeviceEntityTypeConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices", SchemaNames.Events);

            builder
                .Property(x => x.Id)
                .HasField("_id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder
               .Property(x => x.Name)
               .HasField("_name")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .IsRequired()
               .HasMaxLength(150);
        }
    }
}
