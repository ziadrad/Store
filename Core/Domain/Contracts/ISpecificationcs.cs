﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ISpecificationcs<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        Expression<Func<TEntity,bool>> Criteria { get; set; }
       List <Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }
        Expression<Func<TEntity, object>>? OrderBy { get; set; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; }

        int Take { get; set; }
        int Skip { get; set; }
        bool IsPagination { get; set; }

    }
}
