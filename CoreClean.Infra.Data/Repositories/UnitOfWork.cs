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
        private readonly ProjectDbContext _projectDbContext;

        public IPhotoRepository Photos { get; private set; }

        public UnitOfWork(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
            Photos = new PhotoRepository(_projectDbContext);
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
