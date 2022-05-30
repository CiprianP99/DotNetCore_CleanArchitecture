using AutoMapper;
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private readonly IMapper _mapper;
        //private readonly UserManager<User> _userManager;

        public PhotoController(IWebHostEnvironment hostEnvironment, IPhotoService photoService,
            ICategoryService categoryService, ICommentService commentService, IMapper mapper,
            IUserService userService)
        {
            _hostEnvironment = hostEnvironment;
            _photoService = photoService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userService = userService;
            _mapper = mapper;
        }
        // GET: PhotoController
        public ActionResult Index()
        {
            var photos = _photoService.GetAll();
            return View(photos);
        }

        // GET: PhotoController/UserIndex
        public ActionResult UserIndex()
        {
            var userID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var photos = _photoService.GetPhotoByUserId(userID);
            return View(photos);
        }

        // GET: PhotoController/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var photo = _photoService.Get(id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // GET: PhotoController/Create
        public ActionResult Create()
        {
            PhotoViewModel pVM = new PhotoViewModel();

            var catList = _categoryService.GetAll().ToList();
            //ViewBag.Categories = new SelectList(catList);
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
        public ActionResult LikePhoto([FromForm] Photo photo)
        {
            photo = _photoService.Find(x => x.Id == photo.Id).FirstOrDefault();
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userService.Get(userId);

            if (User.Identity.IsAuthenticated)
            {
                photo.UserLikes.Add(user);
                _photoService.Save();
                ViewBag.flag = 1;
            }
            return RedirectToAction("Details", "Photo", new { Id = photo.Id });
        }

        [HttpPost]
        public ActionResult UnLikePhoto([FromForm]Photo photo)
        {
            photo = _photoService.Find(x => x.Id == photo.Id).FirstOrDefault();
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = _userService.Get(userId);

            if(User.Identity.IsAuthenticated)
            {
                photo.UserLikes.Remove(user);
                _photoService.Save();

            }
            return RedirectToAction("Details", "Photo", new { Id = photo.Id });
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PhotoController/Delete/5
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
