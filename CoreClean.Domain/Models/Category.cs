using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
  
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
