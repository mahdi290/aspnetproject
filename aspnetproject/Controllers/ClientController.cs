using Microsoft.AspNetCore.Mvc;
using Aspnetproject.model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Asp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClientController> _logger;

        public ClientController(ApplicationDbContext context, ILogger<ClientController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _context.Products.Include(p => p.Category).ToList();
            return View(objProductList);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Client client)
        {
            // Hash the password
            client.PasswordHash = HashPassword(client.PasswordHash);

            // Save the client to the database
            _context.Clients.Add(client);
            _context.SaveChanges();

            // Redirect to the login page after successful registration
            return RedirectToAction("Login", "Client");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Client client)
        {
            // Hash the password
            client.PasswordHash = HashPassword(client.PasswordHash);

            // Check if the client exists in the database
            var dbClient = _context.Clients.FirstOrDefault(c => c.Username == client.Username && c.PasswordHash == client.PasswordHash);

            if (dbClient != null)
            {
                // The client is authenticated
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dbClient.Username)
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

            return RedirectToAction("Index", "Client");
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddToCart()
        {
            // ... add the product to the cart ...

            // Then return the 'AddToCart' view
            return View();
        }

        [HttpPost]
        [Authorize] // Ensure the user is authenticated
        public IActionResult AddToCart(int productId)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    // Handle the case where the product is not found
                    return NotFound("Product not found.");
                }

                // Get the Id of the currently logged-in client
                var clientIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(clientIdString) || !int.TryParse(clientIdString, out int clientId))
                {
                    // Handle the case where clientIdString is null, empty, or not a valid integer
                    return Unauthorized("User identifier is missing or not a valid integer.");
                }

                var cartItem = _context.CartItems.SingleOrDefault(c => c.ProductId == productId && c.ClientId == clientId);
                if (cartItem != null)
                {
                    // If the item is already in the cart, increase the quantity
                    cartItem.Quantity++;
                }
                else
                {
                    // Otherwise, add a new item to the cart
                    _context.CartItems.Add(new CartItem { ProductId = productId, Quantity = 1, ClientId = clientId });
                }
                _context.SaveChanges();

                // Redirect to a different action or follow the PRG (Post-Redirect-Get) pattern
                return RedirectToAction("AddToCart", "Client");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding product to cart: {ex.Message}");
                // Handle the exception and return an appropriate response
                return StatusCode(500, "Internal Server Error");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Client");
        }

        private string HashPassword(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
