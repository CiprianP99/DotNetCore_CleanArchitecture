using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [ForeignKey("Photo")]
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
