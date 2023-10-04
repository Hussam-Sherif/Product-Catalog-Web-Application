using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Catalog_Web_Application.Core.ViewModel;
using Product_Catalog_Web_Application.Data;

namespace Product_Catalog_Web_Application.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public ProductController(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            var record = await _Context.Products
                .Include(p=>p.Categories)
                .ThenInclude(c=>c.Category)
                .FirstOrDefaultAsync(p=>p.ID== id);
            var ViewModel = new ProductVM()
            {
                ID = record.ID,
                Name = record.Name,
                Duration = record.Duration,
                StartDate = record.StartDate,
                Price = record.Price,
                Categories = record.Categories.Select(c => c.Category.Name).ToList()
            };
            return View(ViewModel);
        }
    }
}
