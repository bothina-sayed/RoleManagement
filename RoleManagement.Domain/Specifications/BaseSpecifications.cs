﻿using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Domain.Specifications
{
    public abstract class BaseSpecifications<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<string> Includes { get; private set; } = new List<string>();
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public bool IsTotalCountEnable { get; private set; }

        protected void AddInclude(List<string> incldueExpression)
        {
            Includes.AddRange(incldueExpression);
        }

        protected void AddCriteria(Expression<Func<T, bool>> criteriaExpression)
        {
            if (Criteria == null)
                Criteria = criteriaExpression;
            else
                Criteria = Criteria.And(criteriaExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int PageSize, int PageIndex)
        {
            Skip = PageSize * (PageIndex - 1);
            Take = PageSize;
            IsPagingEnabled = true;
            EnableTotalCount();
        }

        protected void EnableTotalCount()
        {
            IsTotalCountEnable = true;
        }
    }
}
