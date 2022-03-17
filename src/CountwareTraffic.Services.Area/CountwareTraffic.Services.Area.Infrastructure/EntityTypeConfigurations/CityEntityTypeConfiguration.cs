using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities", SchemaNames.Areas);

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

            builder.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId);
        }
    }
}
