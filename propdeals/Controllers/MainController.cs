using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PropDealsNew.Data;
using PropDealsNew.Models;
using Newtonsoft.Json;
using System.Net.Mail;

namespace PropDealsNew.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        public class PropertiesController : Controller
        {
            readonly ApplicationDbContext _db;
            private readonly IWebHostEnvironment _hostEnvironment;

            public PropertiesController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
            {
                _hostEnvironment = hostEnvironment;
                _db = db;
            }

            public IActionResult Index()
            {
                List<Ad> adList = _db.Ads.ToList();
                return View(adList);
            }

            public IActionResult Buy()
            {
                return View();
            }

            public IActionResult Rent()
            {
                return View();
            }

        }
    }
}
