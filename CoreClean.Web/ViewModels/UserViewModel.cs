using System;

namespace CoreClean.Web.ViewModels
{
    public class UserViewModel: INotificationEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } 
    }
}
