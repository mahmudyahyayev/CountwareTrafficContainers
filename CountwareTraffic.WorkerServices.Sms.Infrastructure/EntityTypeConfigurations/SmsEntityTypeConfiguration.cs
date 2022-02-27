using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.WorkerServices.Sms.Infrastructure
{
    public class SmsEntityTypeConfiguration : IEntityTypeConfiguration<Sms>
    {
        public void Configure(EntityTypeBuilder<Sms> builder)
        {
            builder.ToTable("Sms", SchemaNames.Application);

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
               .Property(x => x.Request)
               .HasField("_request")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasMaxLength(2500);

            builder
                .Property(x => x.PhoneNumbers)
                .HasField("_phoneNumbers")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(2500);

            builder
                .Property(x => x.PhoneNumbers)
                .HasField("_userIds")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(2500);

            builder
               .Property(x => x.IsSuccess)
               .HasField("_isSuccess")
               .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}