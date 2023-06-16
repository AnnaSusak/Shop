using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ShopM4_DataMigrations.Data;
using ShopM4_Models;
using ShopM4_Utility;

using ShopM4_DataMigrations.Repository.IRepository;
using ShopM4_DataMigrations.Repository;

namespace ShopM4.Controllers
{
    [Authorize(Roles = PathManager.AdminRole)]
    public class MyModelController : Controller
    {
        //private ApplicationDbContext db;
        private IRepositoryMyModel repositoryMyModel;

        public MyModelController(IRepositoryMyModel repositoryMyModel)
        {
            this.repositoryMyModel = repositoryMyModel;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<MyModel> models = repositoryMyModel.GetAll();

            return View(models);
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }


        // POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MyModel myModel)
        {
            if (ModelState.IsValid)
            {
                repositoryMyModel.Add(myModel);
                repositoryMyModel.Save();

                return RedirectToAction("Index");
            }
            return View(myModel);
        }
        // GET - Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = repositoryMyModel.Find(id.GetValueOrDefault());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        // POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MyModel model)
        {
            if (ModelState.IsValid)  // проверка модели на валидность
            { 

                repositoryMyModel.Update(model);
                repositoryMyModel.Save();

                return RedirectToAction("Index"); 
            }

            return View(model);
        }

        // GET - Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = repositoryMyModel.Find(id.GetValueOrDefault());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var model = repositoryMyModel.Find(id.GetValueOrDefault());

            if (model == null)
            {
                return NotFound();
            }

            repositoryMyModel.Remove(model);
            repositoryMyModel.Save();


            return RedirectToAction("Index");
        }
    }
}

