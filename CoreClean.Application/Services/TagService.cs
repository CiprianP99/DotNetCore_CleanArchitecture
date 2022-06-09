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
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTag(Tag tag)
        {
            _unitOfWork.Tags.Create(tag);
        }

        public void DeleteTag(Tag tag)
        {
            _unitOfWork.Tags.Delete(tag);
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }

        public Tag Get(Guid id)
        {
            return _unitOfWork.Tags.Get(id);
        }
        public IEnumerable<Tag> GetAll()
        {
            return _unitOfWork.Tags.GetAll();
        }
        public IEnumerable<Tag> Find(Expression<Func<Tag, bool>> predicate)
        {
            return _unitOfWork.Tags.Find(predicate);
        }
        public IEnumerable<Tag> GetTagsByPhotoId(Guid id)
        {
            return _unitOfWork.Tags.Find(t => t.PhotoId == id);
        }
    }
}
