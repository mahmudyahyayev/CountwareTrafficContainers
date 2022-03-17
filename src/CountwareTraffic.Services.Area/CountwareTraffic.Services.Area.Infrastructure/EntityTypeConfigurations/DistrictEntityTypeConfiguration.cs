using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class DistrictEntityTypeConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("Districts", SchemaNames.Areas);

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

            builder.HasOne<City>().WithMany().HasForeignKey(x => x.CityId);
        }
    }
}
