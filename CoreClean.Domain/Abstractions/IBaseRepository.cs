using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Abstractions
{
    public interface IBaseRepository<T>
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
