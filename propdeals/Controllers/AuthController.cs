using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PropDealsNew.Data;
using PropDealsNew.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _db;

    public AuthController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Register obj)
    {
        if(ModelState.IsValid)
        {
            _db.Register.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(obj);
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Index");
        }

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Login obj)
    {
        if (ModelState.IsValid)
        {
            var user = await _db.Register.FirstOrDefaultAsync(u => u.Email == obj.Email && u.Password == obj.Password);

            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, obj.Email),
                new Claim("OtherProperties", "ExampleRole")
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties
                {
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authenticationProperties);

                HttpContext.Session.SetString("Id", user.Id.ToString());
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Phone", user.Phone.ToString());

                // Redirect to a custom URL with the ID as a query parameter
                string customUrl = $"http://localhost:4000/{user.Id}";
                return new RedirectResult(customUrl);
            }
        }

        ModelState.AddModelError("", "User Not Found");
        return View(obj);
    }


    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Auth");
    }

 
}
