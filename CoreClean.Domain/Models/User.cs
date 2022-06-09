using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Photo> PhotoLikes { get; set; }
        public virtual ICollection<Album> Albums { get; set; } 
        public virtual ICollection<Follow> Follower { get; set; }
        public virtual ICollection<Follow> Followee { get; set; }

    }
}
