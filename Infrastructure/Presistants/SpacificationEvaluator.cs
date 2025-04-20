using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Presistants
{
    public class SpacificationEvaluator
    {
        public static  IQueryable<TEntity> GetQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecificationcs<TEntity, Tkey> spec) 
            where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery.AsQueryable();
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            query = spec.IncludeExpressions.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression) );

            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
                
            }
            return query;
        }
    }
}
