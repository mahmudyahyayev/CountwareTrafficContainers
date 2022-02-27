using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.WorkerServices.Sms.Data
{
    public class SmsLogEntityTypeConfiguration : IEntityTypeConfiguration<SmsLog>
    {
        public void Configure(EntityTypeBuilder<SmsLog> builder)
        {
            builder.ToTable("SmsLogs", SchemaNames.Application);

            builder
               .Property(x => x.Id)
               .HasField("_id")
               .IsRequired()
               .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
               .Property(x => x.Response)
               .HasField("_response")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(1000);

            builder
               .Property(x => x.SmsBody)
               .HasField("_smsBody")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(2500);

            builder
                .Property(x => x.PhoneNumbers)
                .HasField("_phoneNumbers")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(2500);

            builder
                .Property(x => x.UserIds)
                .HasField("_userIds")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(2500);

            builder
               .Property(x => x.IsOtp)
               .HasField("_isOtp")
               .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
                  .Property<int>("_smsTypeId")
                  .UsePropertyAccessMode(PropertyAccessMode.Field)
                  .HasColumnName("SmsTypeId")
                  .IsRequired();

            builder.HasOne(o => o.SmsType)
                   .WithMany()
                   .HasForeignKey("_smsTypeId");
        }
    }
}