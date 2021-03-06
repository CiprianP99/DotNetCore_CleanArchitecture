using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [InverseProperty("Albums")]
        public virtual User User { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        
    }
}
