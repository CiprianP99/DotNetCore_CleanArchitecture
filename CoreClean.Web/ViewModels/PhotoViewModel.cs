using CoreClean.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreClean.Web.ViewModels
{
    public class PhotoViewModel
    {
        public Guid Id { get; set; }
        public string Format { get; set; }
        public string Name { get; set; }
        public string Resolution { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        //public IEnumerable<SelectListItem> Categories { get; set; }
        public virtual ICollection<TagViewModel> Tags { get; set; }

        public SelectList catlist { get; set; }
        public UserViewModel User { get; set; }

        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
