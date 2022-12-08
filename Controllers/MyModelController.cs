using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Data;
using Shop.Models;
namespace Shop.Controllers
{
    public class MyModelController : Controller
    {
        private ApplicationDbContext db;
        public MyModelController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<MyModel> models = db.MyModel;
            return View(models);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MyModel model)
        {
            if (ModelState.IsValid)
            {
                db.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = db.MyModel.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MyModel model)
        {
            if (ModelState.IsValid)
            {
                db.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = db.MyModel.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var model = db.MyModel.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            db.MyModel.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult Edit()
        {
            return View();
        }
        

        
        
    }
}
