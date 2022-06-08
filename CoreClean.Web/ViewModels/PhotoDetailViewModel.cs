using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreClean.Web.ViewModels
{
    public class PhotoDetailViewModel
    {
        public Guid Id { get; set; }
        public string Format { get; set; }
        public string Name { get; set; }
        public string Resolution { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [InverseProperty("Photos")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Album> Albums { get; set; }

        [InverseProperty("PhotoLikes")]
        public virtual ICollection<User> UserLikes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

        public bool isLiked { get; set; }
    }
}
