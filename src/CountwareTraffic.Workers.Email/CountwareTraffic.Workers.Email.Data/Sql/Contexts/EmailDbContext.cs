using Microsoft.EntityFrameworkCore;

namespace CountwareTraffic.Workers.Email.Data
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions options)
         : base(options) { }

        public virtual DbSet<EmailLog> EmailLogs { get; set; }
        public virtual DbSet<EmailType> EmailTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies(false);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new EmailLogEntityTypeConfiguration());
            builder.ApplyConfiguration(new EmailTypeEntityTypeConfiguration());
        }
    }
}


