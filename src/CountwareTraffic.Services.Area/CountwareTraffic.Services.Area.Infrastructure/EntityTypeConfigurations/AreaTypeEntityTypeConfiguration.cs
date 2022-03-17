using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class AreaTypeEntityTypeConfiguration : IEntityTypeConfiguration<AreaType>
    {
        public void Configure(EntityTypeBuilder<AreaType> builder)
        {
            builder.ToTable("AreaTypes", SchemaNames.Areas);

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
