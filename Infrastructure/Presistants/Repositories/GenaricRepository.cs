using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistants.Data;

namespace Presistants.Repositories
{
    public class GenaricRepository<TEntity, Tkey> : IGenaricRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenaricRepository(StoreDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {

                return TrackChanges ?
                await _context.Product.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync() as IEnumerable<TEntity>
                : await _context.Product.Include(P => P.ProductBrand).Include(P => P.ProductType).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return TrackChanges ? await _context.Set<TEntity>().ToListAsync() : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {

                return await _context.Product.Include(P => P.ProductBrand).Include(P => P.ProductType).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;

            }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecificationcs<TEntity, Tkey> spec, bool TrackChanges = false)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecificationcs<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();

        }
        private IQueryable<TEntity> ApplySpecifications(ISpecificationcs< TEntity, Tkey > spec)
        {

            return SpacificationEvaluator.GetQuery(_context.Set<TEntity>(), spec);
         }

   

        public Task<int> CountAsync(ISpecificationcs<TEntity, Tkey> spec)
        {
            return ApplySpecifications(spec).CountAsync();
        }
    }
}
