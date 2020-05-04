using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Practice_ASP_NET.Models;

namespace Practice_ASP_NET.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Product Product { get; set; }
        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("seller"))
                return RedirectToAction("UserProducts", new { userID = User.Identity.GetUserId() });
            return View();
        }

        [Authorize(Roles = "admin,seller")]
        public IActionResult UserProducts(string userID)
        {
            if (User.Identity.GetUserId() != userID && !User.IsInRole("admin"))
                return View("AccessDenied");
            User m = new User {
                Id = int.Parse(userID)
            };
            return View(m);
        }
        [Authorize(Roles = "admin,seller")]
        public IActionResult Upsert(int? id)
        {
            Product = new Product();
            if (id == null)
            {
                return View(Product);
            }
            Product = _db.Products.FirstOrDefault(u => u.Id == id);
            if (Product == null || Product.UserID != int.Parse(User.Identity.GetUserId()) && User.IsInRole("admin") == false)
            {
                return View("AccessDenied");
            }
            return View(Product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,seller")]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.Identity.GetUserId());
                if (Product.Id == 0)
                {
                    Product.UserID = userId;
                    _db.Products.Add(Product);
                }
                else
                {
                    var p = _db.Products.AsNoTracking().FirstOrDefault(x => x.Id == Product.Id);
                    if (p != null && (p.UserID == userId || User.IsInRole("admin")))
                    {
                        Product.UserID = userId;
                        _db.Products.Update(Product);
                    }
                    else
                    {
                        return View("AccessDenied");
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        [HttpGet]
        [Authorize(Roles = "admin,buyer")]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Products.ToListAsync() });
        }

        [HttpGet]
        [Authorize(Roles = "admin,seller")]
        public async Task<IActionResult> GetByID(string userid)
        {
            if (User.Identity.GetUserId() != userid && !User.IsInRole("admin"))
                return Json(new { succes = false, message = "Access denied" });
            return Json(new { data = await _db.Products.Where(x => x.UserID.ToString() == userid).ToListAsync() });
        }

        [HttpDelete]
        [Authorize(Roles = "admin,seller")]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (product.UserID.ToString() != User.Identity.GetUserId() && !User.IsInRole("admin"))
            {
                return Json(new { success = false, message = "Access denied" });
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}