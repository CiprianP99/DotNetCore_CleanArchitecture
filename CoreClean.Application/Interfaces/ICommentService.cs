using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Interfaces
{
    public interface ICommentService
    {
        void AddComment(Comment comment);
        void DeleteComment(Comment comment);
        void UpdateComment(Comment comment);
        void Save();

        Comment Get(Guid id);
        IEnumerable<Comment> GetAll();
        IEnumerable<Comment> Find(Expression<Func<Comment, bool>> predicate);
    }
}
