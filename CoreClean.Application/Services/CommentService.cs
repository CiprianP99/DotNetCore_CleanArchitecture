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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddComment(Comment comment)
        {
            _unitOfWork.Comments.Create(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _unitOfWork.Comments.Delete(comment);
        }

        public void UpdateComment(Comment comment)
        {
            _unitOfWork.Comments.Update(comment);
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }

        public IEnumerable<Comment> Find(Expression<Func<Comment, bool>> predicate)
        {
            return _unitOfWork.Comments.Find(predicate);
        }

        public Comment Get(Guid id)
        {
            return _unitOfWork.Comments.Get(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _unitOfWork.Comments.GetAll();
        }

       
    }
}
