using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace ApiProject.Infrastructure.Repository
{
    public class ApiProjectRepository : IApiProjectRepository
    {
        protected readonly DbContext Context;

        public ApiProjectRepository(IApiProjectContext context)
        {
            Context = (DbContext)context;
        }

        public bool Any<T>(Expression<Func<T, bool>> predicate) where T : DbEntity
        {
            return Context.Set<T>().AsNoTracking().Any(predicate);
        }

        public void Delete<T>(T entity) where T : DbEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entities = Context.Set<T>();
            entities.Remove(entity);
        }

        public void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : DbEntity
        {
            var entities = Context.Set<T>().Where(predicate);

            Context.RemoveRange(entities);
        }

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : DbEntity
        {
            IQueryable<T> query = Context.Set<T>();

            return query.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public Task<DateTime> MaxDateAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> select) where T : DbEntity
        {
            IQueryable<T> query = Context.Set<T>();

            if (!query.AsNoTracking().Where(predicate).Any())
                return Task.FromResult(DateTime.MinValue);

            return query.AsNoTracking().Where(predicate).MaxAsync(select);
        }

        public Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : DbEntity
        {
            IQueryable<T> query = Context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Add<T>(T entity) where T : DbEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entities = Context.Set<T>();

            entities.Add(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : DbEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entities = Context.Set<T>();

            entities.AddRange(entity);
        }

        public void SaveChanges(bool clearChanges = false)
        {
            Context.SaveChanges();

            if (clearChanges)
            {
                Context.ChangeTracker.Clear();
            }
        }
    }
}
