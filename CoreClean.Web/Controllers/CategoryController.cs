using AutoMapper;
using CoreClean.Application.Interfaces;
using CoreClean.Domain.Models;
using CoreClean.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreClean.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var cat = _categoryService.GetAll();
            return View(cat);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] CategoryViewModel category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var cat = _mapper.Map<Category>(category);
                _categoryService.AddCategory(cat);
                _categoryService.GetC().Complete();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
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

        // GET: CategoryController/Delete/5
        public ActionResult Delete(Guid id)
        {
            if(id == Guid.Empty)
            {
                return NotFound();
            }
            var category = _categoryService.Get(id);
            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var category = _categoryService.Get(id);
                _categoryService.DeleteCategory(category);
                _categoryService.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
