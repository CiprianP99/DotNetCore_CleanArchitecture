using CoreClean.Domain.Enums;
using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreClean.Web.ViewModels
{
    public class NotificationViewModel
    {
        public Guid Id { get; set; }
        public EntityType Type {get; set; }
        public DateTime Date { get; set; }
        public Guid EntityId { get; set; }
        public bool IsRead { get; set; }

        public Guid UserId { get; set; }
        public UserViewModel User { get; set; }

        public INotificationEntity Entity { get; set; }
    }
}