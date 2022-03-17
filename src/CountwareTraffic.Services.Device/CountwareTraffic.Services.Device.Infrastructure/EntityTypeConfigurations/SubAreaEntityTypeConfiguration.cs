using CountwareTraffic.Services.Devices.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class SubAreaEntityTypeConfiguration : IEntityTypeConfiguration<SubArea>
    {
        public void Configure(EntityTypeBuilder<SubArea> builder)
        {
            builder.ToTable("SubAreas", SchemaNames.Devices);

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
