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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            return _unitOfWork.Users.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.Users.GetAll();
        }
        public IEnumerable<User> GetFollowers(Guid id)
        {
            return _unitOfWork.Users.Find(u => u.Follower.Any(f => f.FollowerId == id)).ToList();
        }
        public IEnumerable<User> GetFollowing(Guid id)
        {
            return _unitOfWork.Users.Find(u => u.Followee.Any(f => f.FolloweeId == id)).ToList();
        }
    }
}
