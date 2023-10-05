using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Product_Catalog_Web_Application.Core.Models;
using Product_Catalog_Web_Application.Data;
using System.Diagnostics;

namespace Product_Catalog_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IToastNotification _toast;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, IToastNotification toast)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _toast = toast;
        }

        public async Task<IActionResult> Index()
        {
 
            //var userID = _userManager.GetUserId(HttpContext.User);
            //if (userID == null)
            //{
            //    var record = await _context.Products
            //    .Where(p => p.StartDate <= DateTime.Now && p.StartDate.AddDays(p.Duration) >= DateTime.Now)
            //    .Include(p => p.Categories)
            //    .ThenInclude(c => c.Category)
            //    .ToListAsync();
            //    return View(record);
            //}
            //else
            //{
            //    var record = await _context.Products
            //   .Include(p => p.Categories)
            //   .ThenInclude(c => c.Category)
            //   .ToListAsync();
            //    return View(record);
            //}

            IQueryable<Product> record = _context.Products.Include(p => p.Categories)
                .ThenInclude(c => c.Category);

            if (User.IsInRole("Admin"))
            {
                record = record.Where(p => p.StartDate <= DateTime.Now && p.StartDate.AddDays(p.Duration) >= DateTime.Now);
            }

            var products = await record.ToListAsync();

            return View(products);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}