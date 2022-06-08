using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Format { get; set; }
        public string Name { get; set; }
        public string Resolution { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [InverseProperty("Photos")]
        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        //[InverseProperty("Albums")]
        public virtual ICollection<Album> Albums { get; set; }
        
        [InverseProperty("PhotoLikes")]
        public virtual ICollection<User> UserLikes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
