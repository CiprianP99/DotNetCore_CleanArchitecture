using CoreClean.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreClean.Web.ViewModels
{
    public class AlbumCreationViewModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

        [InverseProperty("Albums")]
        public virtual User User { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual List<SelectListItem> SelectListPhotos { get; set; }
    }
}
