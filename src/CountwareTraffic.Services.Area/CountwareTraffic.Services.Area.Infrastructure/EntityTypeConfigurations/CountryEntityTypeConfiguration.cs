using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries", SchemaNames.Areas);

            builder
               .Property(x => x.Iso)
               .HasField("_iso")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .IsRequired()
               .HasMaxLength(2);

            builder
               .Property(x => x.Iso3)
               .HasField("_iso")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(3);

            builder
               .Property(x => x.IsoNumeric)
               .HasField("_isoNumeric")
               .IsRequired()
               .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
               .Property(x => x.Id)
               .HasField("_id")
               .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
               .Property(x => x.Name)
               .HasField("_name")
               .IsRequired()
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(150);

            builder
               .Property(x => x.Capital)
               .HasField("_capital")
               .IsRequired()
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(100);

            builder
               .Property(x => x.ContinentCode)
               .HasField("_continentCode")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(2);

            builder
               .Property(x => x.CurrencyCode)
               .HasField("_currencyCode")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(3);

            builder.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId);
        }
    }
}
