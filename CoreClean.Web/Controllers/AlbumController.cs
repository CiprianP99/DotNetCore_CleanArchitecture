using AutoMapper;
using CoreClean.Application.Interfaces;
using CoreClean.Domain.Models;
using CoreClean.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using PagedList;

namespace CoreClean.Web.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumService albumService, IPhotoService photoService, IMapper mapper)
        {
            _albumService = albumService;
            _photoService = photoService;
            _mapper = mapper;
        }

        // GET: AlbumController
        public ActionResult Index()
        {
            
            var userID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var albums = _albumService.GetAlbumByUserId(userID);
            return View(albums);
        }

        // GET: AlbumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AlbumController/Create
        public ActionResult Create(int? page)
        {
            var vm = new AlbumCreationViewModel();
            var userID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var photoListHelper = _photoService.GetPhotoByUserId(userID).ToList();
            vm.SelectListPhotos = new List<SelectListItem>();
            vm.Photos = photoListHelper;
            ViewBag.PhotosId = photoListHelper;
            return View(vm);
        }

        // POST: AlbumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]AlbumCreationViewModel album, Guid[] Photos)
        {
            try
            {
  
                var alb = _mapper.Map<Album>(album);
                List<Photo> photoList = new List<Photo>();
                _albumService.AddAlbum(alb);
                foreach(var el in Photos)
                {
                    var tempPhoto = _photoService.Get(el);
                    photoList.Add(tempPhoto);
                }
                alb.Photos = photoList;
                alb.UserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _albumService.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlbumController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AlbumController/Edit/5
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

        // GET: AlbumController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AlbumController/Delete/5
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
