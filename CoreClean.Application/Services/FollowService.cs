using CoreClean.Application.Interfaces;
using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Services
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddFollow(Follow follow)
        {
            _unitOfWork.Follows.Create(follow);
        }

        public void DeleteFollow(Follow follow)
        {
            _unitOfWork.Follows.Delete(follow);
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }

        public Follow Get(Guid id)
        {
            return _unitOfWork.Follows.Get(id);
        }

        public IEnumerable<Follow> GetAll()
        {
            return _unitOfWork.Follows.GetAll();
        }

        public IEnumerable<Follow> GetFollowers(Guid id)
        {
            return _unitOfWork.Follows.Find(f => f.FolloweeId == id).ToList();
        }

        public Follow GetCertainFollow(Guid followerId, Guid followedId)
        {
            return _unitOfWork.Follows.Find(f => f.FollowerId == followerId && f.FolloweeId == followedId).FirstOrDefault();

            
        }
    }
}
