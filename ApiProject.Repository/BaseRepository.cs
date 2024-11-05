using ApiProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace ApiProject.Repository
{
    public class BaseRepository : IRepository
    {
        protected readonly OperationDBContext Context;

        public BaseRepository(OperationDBContext context)
        {
            Context = context;
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

            foreach (var entity in entities)
            {
                Context.Remove(entity);
            }
        }

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : DbEntity
        {
            IQueryable<T> query = Context.Set<T>();

            return query.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate) where T : DbEntity
        {
            IQueryable<T> query = Context.Set<T>();

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
