using System.Linq.Expressions;
using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Infrastructure
{

    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected OpenERPContext _context;

        public GenericRepository(OpenERPContext context)
        {
            this._context = context;
        }
        public virtual T Add(T entity)
        {
            return _context.Add(entity).Entity;
            
        }

        public virtual IEnumerable<T> All()
        {
            return _context.Set<T>().ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public virtual T GetByID(params object[] ids)
        {
            return _context.Find<T>(ids);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            return _context.Update(entity).Entity;
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {

            return _context.Set<T>().AsQueryable().Where(predicate).Any();
        }

    }
}