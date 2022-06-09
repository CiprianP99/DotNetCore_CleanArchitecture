using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Interfaces
{
    public interface ITagService
    {
        void AddTag(Tag tag);
        void DeleteTag(Tag tag);
        void Save();

        Tag Get(Guid id);
        IEnumerable<Tag> GetAll();
        IEnumerable<Tag> Find(Expression<Func<Tag, bool>> predicate);
        IEnumerable<Tag> GetTagsByPhotoId(Guid id);
    }
}
