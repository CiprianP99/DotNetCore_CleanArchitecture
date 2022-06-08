using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Interfaces
{
    public interface IFollowService
    {
        //void Follow(User user);
        //void Unfollow(User user);
        void AddFollow(Follow follow);
        void DeleteFollow(Follow follow);
        void Save();

        Follow Get(Guid id);
        IEnumerable<Follow> GetAll();
        IEnumerable<Follow> GetFollowers(Guid id);
        Follow GetCertainFollow(Guid followerId, Guid followedId);
    }
}
