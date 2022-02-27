using CountwareTraffic.Services.Companies.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class SubAreaStatusEntityTypeConfiguration : IEntityTypeConfiguration<SubAreaStatus>
    {
        public void Configure(EntityTypeBuilder<SubAreaStatus> builder)
        {
            builder.ToTable("SubAreaStatuses", SchemaNames.Areas);

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
