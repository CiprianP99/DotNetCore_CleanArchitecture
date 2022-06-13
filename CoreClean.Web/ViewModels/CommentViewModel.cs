using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Web.ViewModels
{
    public class CommentViewModel: INotificationEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Text { get; set; }
       
        public Guid PhotoId { get; set; }
        public PhotoViewModel Photo { get; set; }

        public Guid UserId { get; set; }
        public UserViewModel User { get; set; }
    }
}
