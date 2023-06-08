using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropDealsNew.Data;
using PropDealsNew.Models;

namespace PropDealsNew.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        readonly ApplicationDbContext _db;
        

        public IndexController(ApplicationDbContext db)
        {
            
            _db = db;
        }
        public IActionResult Index()
        {
            List<Ad> adsList=_db.Ads.ToList();
            List<Property> propLis = _db.Property.ToList();
            ViewModel myView = new ViewModel
            {
                PropertyList=propLis,
                AdvertList=adsList
            };
            return View(myView);
        }

        [HttpPost]
        public IActionResult Search(string query)
        {
            var matchingProperties = _db.Property
                .FromSqlInterpolated($"SELECT * FROM Property WHERE Name LIKE '%{query}%'")
                .ToList();

            // Pass the matching properties to the view
            return View(matchingProperties);
        }

        public IActionResult Card(int? id)
        {
            var propertyObj=_db.Property.Find(id);


            return View(propertyObj);
        }

        public IActionResult Buy() 
        {
            List<Property> propertyList=_db.Property.ToList();  
            return View(propertyList);
        }

        public IActionResult Account()
        {
            string email = HttpContext.Session.GetString("Email");
            string name = HttpContext.Session.GetString("Name");
            string phone = HttpContext.Session.GetString("Phone");
            string id = HttpContext.Session.GetString("Id");
            Register myRegister = new Register();

            ViewModel myObj = new ViewModel
            {
                Email = email,
                Name = name,
                Phone = phone,
                Id=id,
                RegisterObj=myRegister
            };

            return View(myObj);
        }

    }
}
