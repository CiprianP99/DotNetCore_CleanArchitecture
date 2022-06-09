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
        IUserRepository Users { get; }
        IAlbumRepository Albums { get; }
        IFollowRepository Follows { get; }
        ITagRepository Tags { get; }
        int Complete();
    }
}
