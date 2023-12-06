using Microsoft.AspNetCore.Mvc;
using Aspnetproject.model;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            // Hash the password
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(admin.PasswordHash));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                admin.PasswordHash = hash;
            }

            // Save the admin to the database
            _context.Admins.Add(admin);
            _context.SaveChanges();

            // Redirect to the login page after successful registration
            return RedirectToAction("Login", "Admin");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            // Hash the password
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(admin.PasswordHash));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                admin.PasswordHash = hash;
            }

            // Check if the admin exists in the database
            var dbAdmin = _context.Admins.FirstOrDefault(a => a.Username == admin.Username && a.PasswordHash == admin.PasswordHash);

            if (dbAdmin != null)
            {
                // The admin is authenticated
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dbAdmin.Username)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
            else
            {
                // Authentication failed
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
