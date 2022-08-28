using System.Linq.Expressions;

namespace OpenERP.Data.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T GetByID(params object[] ids);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}