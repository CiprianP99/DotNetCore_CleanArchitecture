using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int EntityId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
