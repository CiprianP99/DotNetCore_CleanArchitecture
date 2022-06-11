using CoreClean.Application.Interfaces;
using CoreClean.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace CoreClean.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        //GET: UserController/ViewProfile
        public IActionResult ViewProfile()
        {
            var userID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userService.Get(userID);
            var followers = user.Followee;
            var followerIds = new List<SelectListItem>();
            foreach (var el in followers)
            {
                SelectListItem tempUser = new SelectListItem();
                var tempId = _userService.Get(el.FolloweeId) ;
                tempUser.Value = tempId.Id.ToString();
                tempUser.Text = tempId.Email;
                followerIds.Add(tempUser);

            }


            ViewBag.Followers = followerIds;
            
            
            return View(user);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
