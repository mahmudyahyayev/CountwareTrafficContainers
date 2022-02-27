using CountwareTraffic.Services.Companies.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class SubAreaEntityTypeConfiguration : IEntityTypeConfiguration<SubArea>
    {
        public void Configure(EntityTypeBuilder<SubArea> builder)
        {
            builder.ToTable("SubAreas", SchemaNames.Areas);

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

            builder
               .Property(x => x.Description)
               .HasField("_description")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(2500);


            builder.HasOne<Area>().WithMany().HasForeignKey(x => x.AreaId);

            builder
                  .Property<int>("_subAreaStatus")
                  .UsePropertyAccessMode(PropertyAccessMode.Field)
                  .HasColumnName("SubAreaStatus")
                  .IsRequired();

            builder.HasOne(o => o.SubAreaStatus)
                   .WithMany()
                   .HasForeignKey("_subAreaStatus");
        }
    }
}
