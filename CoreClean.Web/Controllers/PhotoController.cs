using CoreClean.Application.Utilities;
using CoreClean.Domain.Abstractions;
using CoreClean.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreClean.Web.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public PhotoController(IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork)
        {
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
        }
        // GET: PhotoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PhotoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PhotoController/Create
        public ActionResult Create()
        {
            return View();
        }

        private async Task<string> OnPostUploadAsync(IFormFile FormFile)
        {

            var trustedFileNameForFileStorage = Guid.NewGuid().ToString();
            var filePath = Path.Combine(
                _hostEnvironment.WebRootPath, "upload\\photos\\", trustedFileNameForFileStorage);
            filePath += Path.GetExtension(FormFile.FileName);
            
            
            using (var stream = System.IO.File.Create(filePath))
            {
                await FormFile.CopyToAsync(stream);
            }

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
                var result = _unitOfWork.Photos.Create(photo);
                await OnPostUploadAsync(photo.FormFile);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
