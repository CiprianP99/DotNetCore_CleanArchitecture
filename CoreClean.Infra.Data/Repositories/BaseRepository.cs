using CoreClean.Domain.Abstractions;
using CoreClean.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Infra.Data.Repositories
{
    public abstract class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        protected readonly ProjectDbContext _projectDbContext;

        public BaseRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public void Create(T entity)
        {
            _projectDbContext.Set<T>().Add(entity);
            //_projectDbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _projectDbContext.Set<T>().Remove(entity);
            //_projectDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _projectDbContext.Set<T>().Update(entity);
           //_projectDbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _projectDbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _projectDbContext.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _projectDbContext.Set<T>().Where(predicate);
        }
    }
}
