using Bookify.Web.Seeds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Product_Catalog_Web_Application.Core.Models;
using Product_Catalog_Web_Application.Core.ViewModel;
using Product_Catalog_Web_Application.Data;
using Product_Catalog_Web_Application.Data.Migrations;
using System.Data;
using System.Security.Claims;

namespace Product_Catalog_Web_Application.Controllers
{

    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly IToastNotification _toast;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(ApplicationDbContext context,
            IToastNotification toast,
            UserManager<IdentityUser> userManager)
        {
            _Context = context;
            _toast = toast;
            _userManager = userManager;
        }
        
        //public async Task<IActionResult> Index()
        //{
        //    var record = await _context.Products
        //        .Where(p => p.StartDate <= DateTime.Now && p.StartDate.AddDays(p.Duration) >= DateTime.Now)
        //        .Include(p => p.Categories)
        //        .ThenInclude(c => c.Category)
        //        .ToListAsync();

        //    return View(record);
        //}

        public async Task<IActionResult> Details(int id)
        {
            var record = await _Context.Products
                .Include(p => p.Categories)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(p => p.ID == id);


            if (record.StartDate > DateTime.Now || record.StartDate.AddDays(record.Duration) < DateTime.Now)
                if (!User.IsInRole(AppRoles.Admin))
                    return Redirect("/Identity/Account/Login");


            var ViewModel = new ProductVM()
            {
                ID = record.ID,
                Name = record.Name,
                Duration = record.Duration,
                StartDate = record.StartDate,
                Price = record.Price,
                CreatedBy = await _userManager.GetUserNameAsync
                            (await _Context.Users.FirstAsync(x=>x.Id == record.CreatedById)),
            Categories = record.Categories.Select(c => c.Category!.Name).ToList()
            };

            return View(ViewModel);
        }
        [HttpGet]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Create()
        {
            var record = await _Context.Categories.ToListAsync();
            var ViewModel = new ProductFormViewModel()
            {
                StartDate= DateTime.Now,
                Categories = record.Select(SC => new SelectListItem
                {
                    Text = SC.Name,
                    Value = SC.Id.ToString(),
                }).ToList()
                //Categories = record
            };
            
            return View("ProductForm", ViewModel);
        }
        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Create(ProductFormViewModel ViewModel)
        {

            if (!ModelState.IsValid)
            {
                var Category = await _Context.Categories.ToListAsync();
                var CategoriesSelectListItem = Category.Select(SC => new SelectListItem
                {
                    Text = SC.Name,
                    Value = SC.Id.ToString(),
                }).ToList();

                return View("ProductForm", ViewModel);
            }
            var product = new Product()
            {
                Name = ViewModel.Name,
                StartDate = ViewModel.StartDate,
                Duration = ViewModel.Duration,
                Price = ViewModel.Price,
                CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
            CreationDate = DateTime.Now,
            };
             
            foreach (var category in ViewModel.SelectedCategoryIds)
                product.Categories.Add(new ProductCategory { CategoryID = category } );
       
            await _Context.AddAsync(product);
            _Context.SaveChanges();
            _toast.AddSuccessToastMessage("Product Created Successfully !");
            

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _Context.Products
                .Include(p => p.Categories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (product == null)
                return NotFound();
            
            var record = await _Context.Categories.ToListAsync();
           var CategoriesSelectListItem = record.Select(SC => new SelectListItem
            {
                Text = SC.Name,
                Value = SC.Id.ToString(),
            }).ToList();
            var viewModel = new ProductFormViewModel
            {
                ID = product.ID,
                Name = product.Name,
                Duration = product.Duration,
                StartDate = product.StartDate,
                Price = product.Price,
                Categories = CategoriesSelectListItem,
                SelectedCategoryIds = product.Categories.Select(pc => pc.CategoryID).ToList()
            };

            return View("ProductForm", viewModel);
        }
        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Edit(ProductFormViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                var Category = await _Context.Categories.ToListAsync();
                var CategoriesSelectListItem = Category.Select(SC => new SelectListItem
                {
                    Text = SC.Name,
                    Value = SC.Id.ToString(),
                }).ToList();

                return View("ProductForm", viewModel);
            }

           
            var record =await _Context.Products.Include(pc=>pc.Categories).ThenInclude(c=>c.Category).FirstOrDefaultAsync(p=>p.ID== viewModel.ID);
            if (record is null)
                return NotFound();

            record.Name = viewModel.Name;
            record.Price = viewModel.Price;
            record.StartDate = viewModel.StartDate;
            record.Duration = viewModel.Duration;
            record.Categories.Clear();
            foreach (var category in viewModel.SelectedCategoryIds)
                record.Categories.Add(new ProductCategory { CategoryID = category });
            _Context.SaveChanges();
            _toast.AddInfoToastMessage("Product Edited Successfully !");
            return RedirectToAction("Index", "Home");

        }
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult>Delete(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var record = _Context.Products
                .Include(pc => pc.Categories)
                .ThenInclude(c => c.Category)
                .FirstOrDefault(p => p.ID == id);
            if(record is null)
                return NotFound();

            _Context.Products.Remove(record);
            _Context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AllowItem(ProductFormViewModel model)
        {
            var record = _Context.Products.FirstOrDefault(x => x.Name == model.Name);
            var IsAllowed = record is null || record.ID.Equals(model.ID);
            return Json(IsAllowed);
        }
    }
}

