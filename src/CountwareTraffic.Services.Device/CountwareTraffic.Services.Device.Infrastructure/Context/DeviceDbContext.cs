using CountwareTraffic.Services.Devices.Core;
using Microsoft.EntityFrameworkCore;
using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class DeviceDbContext : DbContext, IScopedSelfDependency
    {
        private readonly DeviceDbProvider _provider;

        public DeviceDbContext(DbContextOptions options)
          : base(options) { }

        public DeviceDbContext(DbContextOptions options, DeviceDbProvider provider)
            : base(options) => _provider = provider;


        #region DBSETs

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<SubArea> SubAreas { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public virtual DbSet<DeviceType> DeviceTypes { get; set; }
        public virtual DbSet<DeviceCreationStatus> DeviceCreationStatuses { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        #endregion DBSETs


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);

            if (_provider != null)
                optionsBuilder.AddInterceptors(new SaveChangesInterceptor(_provider));
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new DeviceEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubAreaEntityTypeConfiguration());
            builder.ApplyConfiguration(new DeviceStatusEntityTypeConfiguration());
            builder.ApplyConfiguration(new DeviceTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new DeviceCreationStatusEntityTypeConfiguration());
            builder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }
    }
}
