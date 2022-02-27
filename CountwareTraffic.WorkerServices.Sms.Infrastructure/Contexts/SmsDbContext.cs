using Microsoft.EntityFrameworkCore;

namespace CountwareTraffic.WorkerServices.Sms.Infrastructure
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions options)
         : base(options) { }

        public virtual DbSet<Sms> Sms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies(false);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SmsEntityTypeConfiguration());
        }
    }
}


