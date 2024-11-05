using System.Linq.Expressions;

namespace ApiProject.Interfaces
{
    public interface IRepository
    {
        bool Any<T>(Expression<Func<T, bool>> predicate) where T : DbEntity;
        void Delete<T>(T entity) where T : DbEntity;
        void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : DbEntity;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : DbEntity;

        Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate) where T : DbEntity;

        void Add<T>(T entity) where T : DbEntity;
        void SaveChanges(bool clearChanges = false);
    }
}