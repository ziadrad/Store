﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenaricRepository<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        Task<int> CountAsync(ISpecificationcs<TEntity,TKey> spec);
        Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync( ISpecificationcs<TEntity, TKey> spec ,bool TrackChanges = false);
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetAsync(ISpecificationcs<TEntity, TKey> spec);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
