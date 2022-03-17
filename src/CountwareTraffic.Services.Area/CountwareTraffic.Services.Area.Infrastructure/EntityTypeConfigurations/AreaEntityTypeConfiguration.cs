using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class AreaEntityTypeConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas", SchemaNames.Areas);

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

          

            var addressNavigationBuilder = builder.OwnsOne(x => x.Address);
            builder
               .Navigation(x => x.Address).Metadata.SetField("_address");
            addressNavigationBuilder.Property(x => x.Street).HasMaxLength(100);
            addressNavigationBuilder.Property(x => x.Location).IsRequired(false);


            var contactNavigationBuilder = builder.OwnsOne(x => x.Contact);
            builder
              .Navigation(x => x.Contact).Metadata.SetField("_contact");
            contactNavigationBuilder.Property(x => x.EmailAddress).HasMaxLength(50);
            contactNavigationBuilder.Property(x => x.FaxNumber).HasMaxLength(20);
            contactNavigationBuilder.Property(x => x.GsmNumber).HasMaxLength(10);
            contactNavigationBuilder.Property(x => x.PhoneNumber).HasMaxLength(16);
            
            builder.HasOne<District>().WithMany().HasForeignKey(x => x.DistrictId);

            builder
                   .Property<int>("_areaTypeId")
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .HasColumnName("AreaTypeId")
                   .IsRequired();

            builder.HasOne(o => o.AreaType)
                   .WithMany()
                   .HasForeignKey("_areaTypeId");
        }
    }
}
