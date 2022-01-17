using CoreClean.Application.Interfaces;
using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IUnitOfWork GetC()
        {
            return _unitOfWork;
        }

        public void AddCategory(Category category)
        {
            _unitOfWork.Categories.Create(category);
        }

        public void DeleteCategory(Category category)
        {
            _unitOfWork.Categories.Delete(category);
        }
        
        public void UpdateCategory(Category category)
        {
            _unitOfWork.Categories.Update(category);
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }

        public Category Get(Guid id)
        {
            return _unitOfWork.Categories.Get(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _unitOfWork.Categories.GetAll();
        }

        public Guid GetCategoryIdByName(string Name)
        {
            return _unitOfWork.Categories.Find(c => c.Name == Name).FirstOrDefault().Id;
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            return _unitOfWork.Categories.Find(predicate);
        }




    }
}
