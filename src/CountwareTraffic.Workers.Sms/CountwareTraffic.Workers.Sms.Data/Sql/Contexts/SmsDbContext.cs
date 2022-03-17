using Microsoft.EntityFrameworkCore;

namespace CountwareTraffic.Workers.Sms.Data
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions options)
         : base(options) { }

        public virtual DbSet<SmsLog> SmsLogs { get; set; }
        public virtual DbSet<SmsType> SmsTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies(false);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SmsLogEntityTypeConfiguration());
            builder.ApplyConfiguration(new SmsTypeEntityTypeConfiguration());
        }
    }
}


