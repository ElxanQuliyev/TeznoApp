#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;

using DataAccess;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles ="Admin")]
    public class CategoriesController : Controller
    {
        private readonly CategoryManager _categoryManager;

        public CategoriesController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }





        // GET: AdminPanel/Categories
        public IActionResult Index()
        {
            return View( _categoryManager.GetCategories());
        }

        // GET: AdminPanel/Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
           var selectedCategory= _categoryManager.GetById(id);
            if (selectedCategory == null)
                return NotFound();
            return View(selectedCategory);
        }

        // GET: AdminPanel/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryName,CategoryIcon,IsDeleted,ID")] Category category)
        {
            if (ModelState.IsValid)
            {
               _categoryManager.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: AdminPanel/Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var selectedCategory=_categoryManager.GetById(id.Value);
            if (selectedCategory == null)
                return NotFound();
            return View(selectedCategory);
        }

        // POST: AdminPanel/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryName,CategoryIcon,IsDeleted,ID")] Category category)
        {
            if (id != category.ID)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                   _categoryManager.Update(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryManager.CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: AdminPanel/Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var selectedCategory = _categoryManager.GetById(id);
            if (selectedCategory == null)
                return NotFound();
            return View(selectedCategory);
        }

        // POST: AdminPanel/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var selectedCategory = _categoryManager.GetById(id);
           _categoryManager.Delete(selectedCategory);    
            return RedirectToAction(nameof(Index));
        }

    
    }
}
