
using CoreClean.Application.Interfaces;
using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Enums;
using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCommentNotification(Guid commentId)
        {
            var comment = _unitOfWork.Comments.Get(commentId);
            var photo = _unitOfWork.Photos.Get(comment.PhotoId);

            _unitOfWork.Notifications.Create(new Notification
            {
                Type = EntityType.Comment,
                EntityId = commentId,
                UserId = photo.UserId,
                IsRead = false,
                Date = DateTime.Now
            });

            Save();
        }

        public void AddFollowNotification(Guid followerId, Guid followedId)
        {
             _unitOfWork.Notifications.Create(new Notification
            {
                Type = EntityType.Follow,
                EntityId = followerId,
                UserId = followedId,
                IsRead = false,
                Date = DateTime.Now
            });

            Save();
        }

        public void AddLikeNotification(Guid photoId, Guid userId)
        {
            _unitOfWork.Notifications.Create(new Notification
            {
                Type = EntityType.Like,
                EntityId = photoId,
                UserId = userId,
                IsRead = false,
                Date = DateTime.Now
            });

            Save();
        }

        public void AddPhotoNotification(Guid photoId, Guid userId)
        {
            var date = DateTime.Now;
            var user = _unitOfWork.Users.Get(userId);

            foreach(var follower in user.Follower) {
                _unitOfWork.Notifications.Create(new Notification
                {
                    Type = EntityType.Photo,
                    EntityId = photoId,
                    UserId = follower.FollowerId,
                    IsRead = false,
                    Date = date
                });
            }
           
           Save();
        }

        public void BroadcastNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public void DeleteNotification(Notification notification)
        {
            _unitOfWork.Notifications.Delete(notification);
        }

        public IEnumerable<Notification> Find(Expression<Func<Notification, bool>> predicate)
        {
            return _unitOfWork.Notifications.Find(predicate);
        }

        public IEnumerable<Notification> GetRecentNotificationsForUser(Guid userId)
        {
            return Find(n => n.UserId == userId).OrderByDescending(u => u.Date).Take(10);
        }

        public int GetUnreadNotificationsCountForUser(Guid userId)
        {
            return Find(n => n.UserId == userId).Count();
        }

        public T GetNotificationEntity<T>(Guid entityId)
        {
            switch(typeof(T).Name) {
                case nameof(Comment):
                    // return (T)(object)_unitOfWork.Comments.Get(entityId);
                    return (T)(object)_unitOfWork.Comments.Get(entityId);
                case nameof(Photo):
                    return (T)(object)_unitOfWork.Photos.Get(entityId);
                case nameof(User):
                    return (T)(object)_unitOfWork.Users.Get(entityId);
                default:
                    throw new InvalidOperationException($"Unknown type {typeof(T).FullName}");
            }
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }
    }
}
