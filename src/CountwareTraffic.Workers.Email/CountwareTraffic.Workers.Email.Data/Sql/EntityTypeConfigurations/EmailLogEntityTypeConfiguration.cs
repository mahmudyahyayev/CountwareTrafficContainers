using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountwareTraffic.Workers.Email.Data
{
    public class EmailLogEntityTypeConfiguration : IEntityTypeConfiguration<EmailLog>
    {
        public void Configure(EntityTypeBuilder<EmailLog> builder)
        {
            builder.ToTable("EmailLogs", SchemaNames.Application);

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
                .Property(x => x.EmailSubject)
                .HasField("_emailSubject")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(500);

            builder
                .Property(x => x.EmailBody)
                .HasField("_emailBody")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(2500);

            builder
                .Property(x => x.EmailTo)
                .HasField("_emailTo")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(500);

            builder
                .Property(x => x.IsHtml)
                .HasField("_isHtml")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
                .Property(x => x.UserIds)
                .HasField("_userIds")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(500);

            builder
                .Property<int>("_emailTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("EmailTypeId")
                .IsRequired();

            builder.HasOne(o => o.EmailType)
                .WithMany()
                .HasForeignKey("_emailTypeId");
        }
    }
}