using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using CoreClean.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Infra.Data.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ProjectDbContext projectDbContext)
                : base(projectDbContext)
        {
        }

        //public IEnumerable<Photo> GetMostLikedPhoto(int likes)
        //{
        //    //return ProjectDbContext.Photos.OrderByDescending(c => c.UserLikes).Take(likes).ToList();
        //}

        //public PhotoContext PhotoContext
        //{
        //    get { return ProjectDbContext as PhotoContext; }
        //}
    }
}
