using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Presistants.Data;
using Presistants.Repositories;

namespace Presistants
{
    public class Unit_of_work : IUnit_of_work
    {
        private readonly StoreDbContext _context;
        private readonly ConcurrentDictionary<string,object> _repository;
        public Unit_of_work(StoreDbContext context)
        {
            this._context = context;
            _repository=new ConcurrentDictionary<string, object> ();
        }
        public IGenaricRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        =>(IGenaricRepository<TEntity, Tkey>) _repository.GetOrAdd(typeof(TEntity).Name,new GenaricRepository<TEntity,Tkey> (_context));
        

        public async Task<int> savechangesAsync()
        {
            return  await _context.SaveChangesAsync();
        }
    }
}
