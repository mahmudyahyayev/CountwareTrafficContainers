using CountwareTraffic.Services.Devices.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class DeviceEntityTypeConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices", SchemaNames.Devices);

            builder
               .Property(x => x.Id)
               .HasField("_id")
               .IsRequired()
               .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
               .Property(x => x.Name)
               .HasField("_name")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .IsRequired()
               .HasMaxLength(150);

            builder
               .Property(x => x.Description)
               .HasField("_description")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(2500);

            builder
              .Property(x => x.Model)
              .HasField("_model")
              .UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasMaxLength(130);

            var connnectionInfoNavigationBuilder = builder.OwnsOne(x => x.ConnectionInfo);
            builder
              .Navigation(x => x.ConnectionInfo).Metadata.SetField("_connectionInfo");
            connnectionInfoNavigationBuilder.Property(x => x.Identity).HasMaxLength(50);
            connnectionInfoNavigationBuilder.Property(x => x.IpAddress).HasMaxLength(15);
            connnectionInfoNavigationBuilder.Property(x => x.MacAddress).HasMaxLength(10);
            connnectionInfoNavigationBuilder.Property(x => x.Password).HasMaxLength(16);
            connnectionInfoNavigationBuilder.Property(x => x.Port).HasMaxLength(5);
            connnectionInfoNavigationBuilder.Property(x => x.UniqueId).HasMaxLength(16);


            builder
                .HasOne<SubArea>()
                .WithMany()
                .HasForeignKey(x => x.SubAreaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .Property<int>("_deviceStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("DeviceStatusId")
                .IsRequired();

            builder.HasOne(o => o.DeviceStatus)
                   .WithMany()
                   .HasForeignKey("_deviceStatusId");

            builder
                .Property<int>("_deviceTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("DeviceTypeId")
                .IsRequired();

            builder.HasOne(o => o.DeviceType)
                   .WithMany()
                   .HasForeignKey("_deviceTypeId");

            builder
               .Property<int>("_deviceCreationStatus")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasColumnName("DeviceCreationStatus")
               .IsRequired();

            builder.HasOne(o => o.DeviceCreationStatus)
                   .WithMany()
                   .HasForeignKey("_deviceCreationStatus");
        }
    }
}
