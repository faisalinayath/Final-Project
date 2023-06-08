using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PropDealsNew.Data;
using PropDealsNew.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PropDealsNew.Controllers

{
    [Authorize]
    public class PropertiesController : Controller
    {
        readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;   

        public PropertiesController(ApplicationDbContext db,IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("Email");
            List<Property> propertyList = _db.Property.ToList();

            ViewModel viewModel=new ViewModel
            {
                PropertyList=propertyList,
                Email=email
            };
            return View(viewModel); 
        }
        
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.Photo != null)
                {
                    string folder = "ImagesUpload/";
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;
                    string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folder);
                    string filePath = Path.Combine(serverFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await obj.Photo.CopyToAsync(fileStream);
                    }

                    // Update the Image_Path property of the obj
                    obj.Image_Path = Path.Combine(folder, uniqueFileName);


                }
                
                obj.Seller_Name = HttpContext.Session.GetString("Name");
                obj.Seller_Phone = HttpContext.Session.GetString("Phone");
                obj.Seller_Email_Id = HttpContext.Session.GetString("Email");

                _db.Property.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Properties");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            var property = _db.Property.Find(id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);  
            
        }

        public IActionResult Delete(int? id) 
        {
            var property=_db.Property.Find(id);

            if(property != null)
            {
                _db.Property.Remove(property);  
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(Property obj) 
        {
            if (ModelState.IsValid)
            {
                if (obj.Photo != null)
                {
                    string folder = "ImagesUpload/";
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;
                    string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folder);
                    string filePath = Path.Combine(serverFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await obj.Photo.CopyToAsync(fileStream);
                    }

                    // Update the Image_Path property of the obj
                    obj.Image_Path = Path.Combine(folder, uniqueFileName);
                }

                _db.Property.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Auth");
        }

        public IActionResult Advertise(int? id)
        {
            var property=_db.Property.Find(id);

            Ad adObj = new Ad
            {
                Name=property.Name,
                Price=property.Price,
                Description=property.Description,
                Image_Path=property.Image_Path,
                Seller_Name=property.Seller_Name,
                Seller_Email_Id=property.Seller_Email_Id,
                Seller_Phone=property.Seller_Phone
            };

            _db.Ads.Add(adObj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
