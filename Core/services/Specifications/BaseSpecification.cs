﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace services.Specifications
{
    public class BaseSpecification<TEntity, Tkey> : ISpecificationcs<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        public int Take { get  ; set  ; }
        public int Skip { get  ; set  ; }
        public bool IsPagination { get  ; set  ; }

        public BaseSpecification(Expression<Func<TEntity, bool>> Expression)
        {
            Criteria = Expression;
        }
        protected void AddIncludes(Expression<Func<TEntity, object>> Expression)
        {
            IncludeExpressions.Add(Expression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
        { 
        OrderByDescending = expression;
            }

        protected void ApplyPagination(int pageIndex,int pageSize)
        {
            IsPagination = true;
            Take = pageSize;
            Skip = (pageIndex-1) * pageSize;
        }
    }
}
