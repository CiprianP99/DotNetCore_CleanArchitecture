using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Photo")]
        public Guid PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
