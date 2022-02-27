using CountwareTraffic.Services.Events.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events", SchemaNames.Events);

            builder
                .Property(x => x.Id)
                .HasField("_id")
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
               .Property(x => x.Description)
               .HasField("_description")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(2500);

            builder
              .Property(x => x.EventDate)
              .HasField("_eventDate")
              .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
              .Property(x => x.CreateDate)
              .HasField("_createDate")
              .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
               .Property(x => x.CreatedBy)
               .HasField("_createBy")
               .IsRequired()
               .UsePropertyAccessMode(PropertyAccessMode.Field);


            builder.HasOne<Device>()
                .WithMany()
                .HasForeignKey(x => x.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .Property<int>("_directionTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("DirectionTypeId")
                .IsRequired();

            builder.HasOne(o => o.DirectionType)
                   .WithMany()
                   .HasForeignKey("_directionTypeId");
        }
    }
}
