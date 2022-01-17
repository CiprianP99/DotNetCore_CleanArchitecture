using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IPhotoRepository Photos { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        int Complete();
    }
}
