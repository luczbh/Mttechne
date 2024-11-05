using System.Linq.Expressions;

namespace ApiProject.Infrastructure.Repository
{
    public interface IApiProjectRepository
    {
        bool Any<T>(Expression<Func<T, bool>> predicate) where T : DbEntity;
        void Delete<T>(T entity) where T : DbEntity;
        void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : DbEntity;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : DbEntity;
        Task<DateTime> MaxDateAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> select) where T : DbEntity;
        Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : DbEntity;
        void Add<T>(T entity) where T : DbEntity;
        void AddRange<T>(IEnumerable<T> entity) where T : DbEntity;
        void SaveChanges(bool clearChanges = false);
    }
}