using CountwareTraffic.Services.Companies.Core;
using Microsoft.EntityFrameworkCore;
using Sensormatic.Tool.Efcore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly AreaDbContext _context;
        public Repository(AreaDbContext context) =>
            _context = context;


        public async Task AddAsync(TEntity entity) =>
            await _context.Set<TEntity>()
                          .AddAsync(entity);

        public async Task<TEntity> GetByIdAsync(Guid id) =>
          await _context.Set<TEntity>()
                        .FindAsync(id);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) =>
            await _context.Set<TEntity>()
                          .AddRangeAsync(entities);

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => 
            _context.Set<TEntity>()
                    .Where(predicate);

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _context.Set<TEntity>()
                    .ToListAsync();

        public void Remove(TEntity entity) => 
            _context.Set<TEntity>()
                    .Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) => 
            _context.Set<TEntity>()
                    .RemoveRange(entities);

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => 
            _context.Set<TEntity>()
                    .SingleOrDefaultAsync(predicate);

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null) => 
            GetQuery(orderBy).Where(predicate);



        public IQueryable<TEntity> GetQuery(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query;

            if (typeof(TEntity).GetInterfaces().Any(x => x == typeof(IDeletable)))
                query = ((IQueryable<IDeletable>)_context.Set<TEntity>()
                                                         .AsQueryable())
                                                                        .Where(u => u.AuditIsDeleted == false)
                                                                        .Cast<TEntity>();
            else
                query =  _context.Set<TEntity>()
                                .AsQueryable();

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }
    }
}
