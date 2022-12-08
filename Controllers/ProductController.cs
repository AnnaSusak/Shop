using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Models;
using Shop.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db;
        private IWebHostEnvironment webHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment=webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objlist = db.Product;
            // получаем ссылки на сущности категорий
            /*foreach (var item in objlist)
            {
                //сопоставление таблицы категории и продукта
                item.Category = db.Category.FirstOrDefault(x => x.Id == item.CategoryId);
            }*/
            return View(objlist);
        }

        // GET: ProductController/Create
        public IActionResult CreateEdit(int? id)
        {
            /*
            IEnumerable<SelectListItem> CategoriesList= db.Category.Select(
                x=>new SelectListItem { 
                    Text=x.Name, Value=x.Id.ToString()
                });
            // отправляем лист категорий во View
            // ViewBag.CategoriesList = CategoriesList;
            ViewData["CategoriesList"] = CategoriesList;
            */
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoriesList = db.Category.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                MyModelsList=db.MyModel.Select(
                    x=>new SelectListItem
                    {
                        Text=x.Name,
                        Value=x.Id.ToString()
                    }
                    )
            };
            
            if (id==null)
            {
                // create product
                return View(productViewModel);
            }
            else
            {
                // edit product
                productViewModel.Product = db.Product.Find(id);
                if (productViewModel.Product == null)
                {
                    return NotFound();
                }
                return View(productViewModel);
            }
            
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit( ProductViewModel productViewModel)
        {
            var files=HttpContext.Request.Form.Files;
            string wwwRoot = webHostEnvironment.WebRootPath;
            if (productViewModel.Product.Id==0)
            {
                //create
                string upload = wwwRoot + PathManager.ImageProductPath;
                string imageName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                string path = upload + imageName + extension;
                // скопируем файл на сервер
                using(var fileStream =new FileStream(path, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                productViewModel.Product.Image = imageName + extension;
                db.Product.Add(productViewModel.Product);
            }
            else
            {
                //update
                var product = db.Product.AsNoTracking().FirstOrDefault(u => u.Id == productViewModel.Product.Id);
                if (files.Count>0)
                {
                    string upload = wwwRoot + PathManager.ImageProductPath;
                    string imageName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    string path = upload + imageName + extension;
                    //delete old file
                    var oldFile = upload+product.Image;
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productViewModel.Product.Image = imageName + extension;
                }
                else //фотка не поменялась
                {
                    productViewModel.Product.Image = product.Image; //оставляем имя прежним
                    db.Product.Update(productViewModel.Product);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
           // return View();
        }


        // GET Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            Product product = db.Product.Find(id);
            if (product==null)
            {
                return NotFound();
            }
            product.Category = db.Category.Find(product.CategoryId);
            product.MyModel = db.MyModel.Find(product.MyModelId);
            return View(product);
        }

        // POST Delete
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            string wwwRoot = webHostEnvironment.WebRootPath;
            string upload = wwwRoot + PathManager.ImageProductPath;
            var oldFile = upload + product.Image;
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
