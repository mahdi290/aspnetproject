using Asp;
using Aspnetproject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace aspnetproject.Controllers
{
    [Authorize]

    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _db.Products.Include(p => p.Category).ToList();
            return View(objProductList);
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            try
            {
                if (_db.Products.Where(x => x.Title == obj.Title).Count() > 0)
                {
                    ViewBag.error = "existe prod";
                    return View(obj);
                }
                _db.Products.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

            // ViewBag.Categories = _db.Categories.ToList();

        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var productFromDb = _db.Products
                .Include(p => p.Category)
                .FirstOrDefault(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _db.Categories.ToList();
            return View(productFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            try
            {
                if (_db.Products.Where(x => x.Title == obj.Title && x.Id != obj.Id).Any())
                {
                    ViewBag.error = "Product with the same title already exists.";
                    ViewBag.Categories = _db.Categories.ToList();
                    return View(obj);
                }

                _db.Entry(obj).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Categories = _db.Categories.ToList();
                return View(obj);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var productFromDb = _db.Products.Include(p => p.Category).FirstOrDefault(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Products.FirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Products.Remove(obj);

            _db.SaveChanges();

            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
