using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Workers.Sms.Data
{
    public class SmsTypeEntityTypeConfiguration : IEntityTypeConfiguration<SmsType>
    {
        public void Configure(EntityTypeBuilder<SmsType> builder)
        {
            builder.ToTable("SmsTypes", SchemaNames.Application);

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
