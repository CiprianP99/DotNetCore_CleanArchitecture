using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
    public class Follow
    {
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }

        public virtual User Follower { get; set; }
        public virtual User Followee { get; set; }
    }
}
