using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Interfaces
{
    public interface ICategoryService
    {
        IUnitOfWork GetC();

        void AddCategory(Category cat);
        void DeleteCategory(Category cat);
        void UpdateCategory(Category cat);
        void Save();

        Category Get(Guid id);
        IEnumerable<Category> GetAll();
        Guid GetCategoryIdByName(string Name);
        IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate);

    }
}
