using Microsoft.EntityFrameworkCore;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Specifications;
using RoleManagement.Infrastructure.Context;
using RoleManagement.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Infrastructure.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        internal DbSet<T> _entity;
        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async Task Add(T entity) => await _entity.AddAsync(entity);
        public async Task AddRange(IEnumerable<T> entities) => await _entity.AddRangeAsync(entities);
        public void Update(T entity) => _entity.Update(entity);
        public void UpdateRange(IEnumerable<T> entity) => _entity.UpdateRange(entity);
        public void Delete(T entity) => _entity.Remove(entity);
        public void DeleteRange(IEnumerable<T> entity) => _entity.RemoveRange(entity);
        public IReadOnlyList<T> Get() => _entity.AsNoTracking().ToList();
        public (IQueryable<T> data, int count) GetWithSpec(BaseSpecifications<T> specifications) => SpecificationEvaluator<T>.GetQuery(_entity, specifications);
        public async Task<T?> GetById(params object[] idValues) => await _entity.FindAsync(idValues);
        public (T? data, int count) GetEntityWithSpec(BaseSpecifications<T> specifications)
        {
            var result = SpecificationEvaluator<T>.GetQuery(_entity, specifications);

            var data = result.data.FirstOrDefault();

            var count = data == null ? 0 : 1;

            return (data, count);
        }
        public async Task<T?> GetByGuid(Guid id) => await _entity.FindAsync(id);
        public async Task<T?> GetObj(Expression<Func<T, bool>> filter) => await _entity.AsQueryable<T>().FirstOrDefaultAsync(filter);
        public async Task<bool> IsExist(Expression<Func<T, bool>> filter) => await _entity.AnyAsync(filter);
        public async Task<bool> Save() => await _context.SaveChangesAsync() > 0;
    }
}
