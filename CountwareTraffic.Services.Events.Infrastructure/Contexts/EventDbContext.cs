using CountwareTraffic.Services.Events.Core;
using Microsoft.EntityFrameworkCore;
using System;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventDbContext : DbContext
    {
        private readonly EventDbProvider _provider;

        public EventDbContext(DbContextOptions options)
         : base(options) { }

        public EventDbContext(DbContextOptions options, EventDbProvider provider)
            : base(options) => _provider = provider;


        #region DBSETs

        public virtual DbSet<Event> Accounts { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DirectionType> DirectionTypes { get; set; }
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
            builder.ApplyConfiguration(new EventEntityTypeConfiguration());
            builder.ApplyConfiguration(new DirectionTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }
    }
}


