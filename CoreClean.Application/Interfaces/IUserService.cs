using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Interfaces
{
    public interface IUserService
    {
        void Save();

        User Get(Guid id);
        IEnumerable<User> GetAll();
        IEnumerable<User> Find(Expression<Func<User, bool>> predicate);
    }
}
