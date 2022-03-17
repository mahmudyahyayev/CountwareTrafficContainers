using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class AreaDbContext : DbContext
    {
        private readonly AreaDbProvider _provider;

        public AreaDbContext(DbContextOptions options)
          : base(options) { }

        public AreaDbContext(DbContextOptions options, AreaDbProvider provider)
            : base(options) => _provider = provider;


        #region DBSETs

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<CountwareTraffic.Services.Areas.Core.Area> Areas { get; set; }
        public virtual DbSet<AreaType> AreaTypes { get; set; }
        public virtual DbSet<SubArea> SubAreas { get; set; }
        public DbSet<SubAreaStatus> SubAreaStatuses { get; set; }
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
            builder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
            builder.ApplyConfiguration(new CountryEntityTypeConfiguration());
            builder.ApplyConfiguration(new CityEntityTypeConfiguration());
            builder.ApplyConfiguration(new DistrictEntityTypeConfiguration());
            builder.ApplyConfiguration(new AreaEntityTypeConfiguration());
            builder.ApplyConfiguration(new AreaTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubAreaEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubAreaStatusEntityTypeConfiguration());
            builder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }
    }
}
