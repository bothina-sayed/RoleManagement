using Microsoft.EntityFrameworkCore;
using RoleManagement.Domain.Specifications;


namespace RoleManagement.Infrastructure.Specifications
{
    internal class SpecificationEvaluator<T> where T : class
    {
        public static (IQueryable<T> data, int count) GetQuery(IQueryable<T> inputQuery,
            BaseSpecifications<T> specifications)
        {
            var query = inputQuery;
            int Count = 0;

            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);

            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IsTotalCountEnable)
                Count = query.Count();

            if (specifications.IsPagingEnabled)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            query = specifications.Includes
                .Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            return (query, Count);
        }
    }
}
