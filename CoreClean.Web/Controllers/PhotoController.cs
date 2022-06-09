﻿using AutoMapper;
using CoreClean.Application.Interfaces;
using CoreClean.Application.Utilities;
using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
//using CoreClean.Domain.Models;
using CoreClean.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreClean.Web.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPhotoService _photoService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IFollowService _followService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        //private readonly UserManager<User> _userManager;

        public PhotoController(IWebHostEnvironment hostEnvironment, IPhotoService photoService,
            ICategoryService categoryService, ICommentService commentService, IMapper mapper,
            IFollowService followService, IUserService userService, ITagService tagService)
        {
            _hostEnvironment = hostEnvironment;
            _photoService = photoService;
            _categoryService = categoryService;
            _commentService = commentService;
            _followService = followService;
            _userService = userService;
            _tagService = tagService;
            _mapper = mapper;
        }
        // GET: PhotoController
        public ActionResult Index(string sortOrder, string search, List<Guid> catId, int? i)
        {
            var photos = _photoService.GetAll();
            var tags = _tagService.GetAll();
            var categories = _categoryService.GetAll();
            ViewBag.OrderOptions = new List<SelectListItem>()
            {
                new SelectListItem { Value = "nameAsc", Text = "Title Ascending" },
                new SelectListItem { Value = "nameDesc", Text = "Title Descending" },
                new SelectListItem { Value = "likeAsc", Text = "Like Ascending" },
                new SelectListItem { Value = "likeDesc", Text = "Like Descending" },
                new SelectListItem { Value = "dateAsc", Text = "Date Ascending" },
                new SelectListItem { Value = "dateDesc", Text = "Date Descending" }

            };
            ViewBag.CurrentOrdering = sortOrder;
            ViewBag.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name}).ToList();
            ViewBag.CurrentCat = catId;
          
            //var result = from t in tags
            //             join p in photos on t.PhotoId equals p.Id
            //             where t.Name == search
            //             select photos;
            if(search != null)
            {
                photos = photos.Where(p => p.Tags.Any(t => t.Name.Contains(search)));
            }
            if(catId?.Any() == true)
            {
                photos = photos.Where(p => catId.Contains(p.CategoryId));
            }
            switch(sortOrder)
            {
                case "nameDesc":
                    photos = photos.OrderByDescending(p => p.Title);
                    break;
                case "nameAsc":
                    photos = photos.OrderBy(p => p.Title);
                    break;
                case "likeAsc":
                    photos = photos.OrderBy(p => p.UserLikes.Count());
                    break;
                case "likeDesc":
                    photos = photos.OrderByDescending(p => p.UserLikes.Count());
                    break;
                case "dateAsc":
                    photos = photos.OrderBy(p => p.DatePublished);
                    break;
                case "dateDesc":
                    photos = photos.OrderByDescending(p => p.DatePublished);
                    break;
                default:
                    photos = photos.OrderByDescending(p => p.Id);
                    break;

            }
            
            return View(photos);
        }

        // GET: PhotoController/UserIndex
        public ActionResult UserIndex()
        {
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
            {
                var userID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var photos = _photoService.GetPhotoByUserId(userID);
                return View(photos);
            }
            return View();
        }

        //GET: PhotoController/UserFavorites
        public ActionResult UserFavorites()
        {
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
            {
                var userID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                User user = _userService.Get(userID);
                List<Photo> photoList = new List<Photo>();
                photoList = user.PhotoLikes.ToList();
                return View(photoList);
            }

            return View();

        }

        // GET: PhotoController/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userService.Get(userId);
            var photoS = _photoService.Get(id);
            var followedUserId = photoS.UserId;
            var followedUser = _userService.Get(followedUserId);

            if (_followService.GetCertainFollow(userId, followedUserId) != null)
            {
                TempData["boolFlag"] = "true";
            }
            else
            {
                TempData["boolFlag"] = "false";
            }

            PhotoDetailViewModel pVM = new PhotoDetailViewModel();

            var photo = _photoService.Get(id);

            pVM = _mapper.Map<PhotoDetailViewModel>(photo);

            if (photo == null)
            {
                return NotFound();
            }
            return View(pVM);
        }

        // GET: PhotoController/Create
        public ActionResult Create()
        {
            PhotoViewModel pVM = new PhotoViewModel();

            var catList = _categoryService.GetAll().ToList();
            pVM.catlist = new SelectList(catList, "Id", "Name");
            return View(pVM);
        }

        private async Task<string> OnPostUploadAsync(IFormFile FormFile)
        {

            var trustedFileNameForFileStorage = Guid.NewGuid().ToString();
            var filePath = Path.Combine(
                _hostEnvironment.WebRootPath, "upload\\photos\\", trustedFileNameForFileStorage);
            filePath += Path.GetExtension(FormFile.FileName).ToLower();
            using (var stream = System.IO.File.Create(filePath))
            {
                await FormFile.CopyToAsync(stream);
            }
            filePath = trustedFileNameForFileStorage;
            filePath += Path.GetExtension(FormFile.FileName).ToLower();
            return filePath;
        }


        public Task<HttpResponseMessage> UploadAsFormDataContent(string url, byte[] image)
        {
            MultipartFormDataContent form = new MultipartFormDataContent
                        {
        { new ByteArrayContent(image, 0, image.Length), "photo", "pic.jpeg" }
                        };

            HttpClient client = new HttpClient();
            return client.PostAsync(url, form);
        }

        // POST: PhotoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] PhotoViewModel photo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var ph = _mapper.Map<Photo>(photo);
                _photoService.AddPhoto(ph);
                ph.UserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var photoPath = await OnPostUploadAsync(photo.FormFile);
                ph.Name = photoPath;
                string fPath = Path.Combine(_hostEnvironment.WebRootPath, "upload\\photos\\", photoPath);
                var img = Image.FromFile(fPath);
                var height = img.Height.ToString();
                var width = img.Width.ToString();
                
                var resolution = width + "x" + height;
                ph.Resolution = resolution;
                ph.Format = Path.GetExtension(fPath).ToLower();
                ph.DatePublished = DateTime.Now;
                var response = await UploadAsFormDataContent("http://127.0.0.1:5000/imageclassifier/predict/", img.ImageToByteArray());
                var contents = await response.Content.ReadAsStringAsync();
                var listString = contents.Split(',').ToList();
                foreach(var el in listString)
                {
                    Tag newTag = new Tag();
                    newTag.Name = el;
                    ph.Tags.Add(newTag);
                }
                _photoService.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult AddComment([FromForm] Comment comment)
        {
            if (String.IsNullOrWhiteSpace(comment.Text))
            {
                return BadRequest("Cannot add empty comment");
            }
            comment.UserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _commentService.AddComment(comment);
            _commentService.Save();
            return RedirectToAction("Details", "Photo", new { Id = comment.PhotoId });
        }

        [HttpPost]
        public ActionResult Follow([FromForm] Photo photo)
        {
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userService.Get(userId);
            var photoS = _photoService.Get(photo.Id);
            var followedUserId = photoS.UserId;
            var followedUser = _userService.Get(followedUserId);


            if (User.Identity.IsAuthenticated && user.Followee.Any(x => x.FolloweeId == followedUserId))
            {
                var certainFollow = _followService.GetCertainFollow(userId, followedUserId);
                //var followw = user.Follower.Select(x => x.FolloweeId == followedUserId).FirstOrDefault();
                _followService.DeleteFollow(certainFollow);
                _followService.Save();



                //user.Followee.Remove(query);
            }
            else
            {
                Follow newFollow = new Follow();
                _followService.AddFollow(newFollow);
                newFollow.Follower = user;
                newFollow.Followee = followedUser;
                _followService.Save();
            }

            //Follow newFollow = new Follow();


            return RedirectToAction("Details", "Photo", new { Id = photo.Id });
        }

        [HttpPost]
        public ActionResult LikePhoto([FromForm] PhotoDetailViewModel photo)
        {

            var photoS = _photoService.Find(x => x.Id == photo.Id).FirstOrDefault();

            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userService.Get(userId);

            PhotoDetailViewModel pVM = new PhotoDetailViewModel();
            pVM = _mapper.Map<PhotoDetailViewModel>(photoS);

            //if (User.Identity.IsAuthenticated && _photoService.Find(x => x.UserLikes.Contains(user)).Any())
            if (User.Identity.IsAuthenticated && pVM.UserLikes.Any(x => x.Id == userId))
            {

                photoS.UserLikes.Remove(user);
                _photoService.Save();
                pVM.isLiked = false;

            }
            else
            {
                photoS.UserLikes.Add(user);
                _photoService.Save();
                pVM.isLiked = true;
            }
            return RedirectToAction("Details", "Photo", new { Id = pVM.Id });
        }

        // GET: PhotoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PhotoController/Edit/5
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

        // GET: PhotoController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var photo = _photoService.Get(id);

            return View(photo);
        }

        // POST: PhotoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var photo = _photoService.Get(id);
                var comms = photo.Comments;
                if (comms != null)
                {
                    foreach (var el in comms)
                    {
                        _commentService.DeleteComment(el);
                    }
                }
                _commentService.Save();

                if (photo.UserLikes != null)
                {
                    var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    User user = _userService.Get(userId);
                    photo.UserLikes.Remove(user);
                }

                _photoService.DeletePhoto(photo);
                _photoService.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
