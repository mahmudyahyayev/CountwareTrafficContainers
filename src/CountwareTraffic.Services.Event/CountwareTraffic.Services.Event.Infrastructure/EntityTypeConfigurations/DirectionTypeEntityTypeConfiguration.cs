using CountwareTraffic.Services.Events.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class DirectionTypeEntityTypeConfiguration : IEntityTypeConfiguration<DirectionType>
    {
        public void Configure(EntityTypeBuilder<DirectionType> builder)
        {
            builder.ToTable("DirectionTypes", SchemaNames.Events);

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
