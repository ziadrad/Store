using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnit_of_work
    {
        Task<int> savechangesAsync();
        IGenaricRepository<TEntity,Tkey> GetRepository<TEntity, Tkey>() where TEntity:BaseEntity<Tkey> ;
    }
}
