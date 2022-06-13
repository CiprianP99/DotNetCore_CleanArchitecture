using AutoMapper;
using CoreClean.Application.Interfaces;
using CoreClean.Domain.Enums;
using CoreClean.Domain.Models;
using CoreClean.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CoreClean.Web.Controllers
{
    public class NotificationController : Controller
    {

        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }


        // GET: NotificationController
        public ActionResult Index()
        {
            _notificationService.GetNotificationEntity<Comment>(new Guid());
            return View();
        }


        private List<NotificationViewModel> GetNotificationViewModels(List<Notification> notifications) {
            var vms = notifications.Select(e => _mapper.Map<NotificationViewModel>(e)).ToList();
            foreach(var vm in vms) {
                switch(vm.Type) {
                    case EntityType.Comment:
                        // You've received a new comment on the photo x from y
                        var comment = _notificationService.GetNotificationEntity<Comment>(vm.EntityId);
                        vm.Entity = _mapper.Map<CommentViewModel>(comment);
                        break;
                    case EntityType.Like:
                        // X liked ur photo
                        vm.Entity = _mapper.Map<PhotoViewModel>(_notificationService.GetNotificationEntity<Photo>(vm.EntityId));
                        break;
                    case EntityType.Follow:
                        // X followed you
                        vm.Entity = _mapper.Map<UserViewModel>(_notificationService.GetNotificationEntity<User>(vm.EntityId));
                        break;
                    case EntityType.Photo:
                        // X posted "Y"
                        vm.Entity = _mapper.Map<PhotoViewModel>(_notificationService.GetNotificationEntity<Photo>(vm.EntityId));
                        break;
                }
            }

            return vms.ToList();
        }


        public ActionResult GetNotificationsPartial() {
            var user = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var notifications = _notificationService.GetRecentNotificationsForUser(user);
            var notificationsCount = _notificationService.GetUnreadNotificationsCountForUser(user);

            ViewBag.NotificationCount = notificationsCount;
            return PartialView("_NotificationsPartial", GetNotificationViewModels(notifications.ToList()));
        }

        // GET: NotificationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        

        // GET: NotificationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotificationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotificationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
