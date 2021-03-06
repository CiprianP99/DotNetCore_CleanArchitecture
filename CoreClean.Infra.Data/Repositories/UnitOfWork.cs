using CoreClean.Domain.Abstractions;
using CoreClean.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProjectDbContext _projectDbContext;

        public IPhotoRepository Photos { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IUserRepository Users { get; private set; }
        public IAlbumRepository Albums { get; private set; }
        public IFollowRepository Follows { get; private set; }
        public ITagRepository Tags { get; private set; }
        public INotificationRepository Notifications { get; private set; }

        public UnitOfWork(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
            Photos = new PhotoRepository(_projectDbContext);
            Categories = new CategoryRepository(_projectDbContext);
            Comments = new CommentRepository(_projectDbContext);
            Users = new UserRepository(_projectDbContext);
            Albums = new AlbumRepository(_projectDbContext);
            Follows = new FollowRepository(_projectDbContext);
            Tags = new TagRepository(_projectDbContext);
            Notifications = new NotificationRepository(_projectDbContext);
        }

        public int Complete()
        {
            return _projectDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _projectDbContext.Dispose();
        }
    }
}
