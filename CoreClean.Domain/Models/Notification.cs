using CoreClean.Domain.Enums;
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
        public EntityType Type {get; set; }
        public DateTime Date { get; set; }
        public Guid EntityId { get; set; }
        public bool IsRead { get; set; }
        [InverseProperty("InitiatedNotifications")]
        public virtual User? InitiatorUser { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
