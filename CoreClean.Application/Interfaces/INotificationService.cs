using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Interfaces
{
    public interface INotificationService
    {
   
        void AddLikeNotification(Guid photoId, Guid userId);
        void AddFollowNotification(Guid followerId, Guid followedId);
        void AddPhotoNotification(Guid photoId, Guid userId);
        void AddCommentNotification(Guid commentId);
        void BroadcastNotification(Notification notification);
        void DeleteNotification(Notification notification);


        IEnumerable<Notification> Find(Expression<Func<Notification, bool>> predicate);
        IEnumerable<Notification> GetRecentNotificationsForUser(Guid userId);
        int GetUnreadNotificationsCountForUser(Guid userId);

        void Save();

        T GetNotificationEntity<T>(Guid entityId);
    }
}
