using Microsoft.AspNetCore.Identity;
using Product_Catalog_Web_Application.Core.Models;
using Product_Catalog_Web_Application.Data;

namespace Product_Catalog_Web_Application.Seeds
{
    public class DefaultCategory
    {
        public static async Task SeedCategory(ApplicationDbContext _db)
        {
            if (!_db.Categories.Any())
            {
                var initialCategories = new[]
                 {
                new Category { Name = "Category 1"},
                new Category { Name = "Category 2"},
                };
                // Add more categories as needed

                _db.Categories.AddRange(initialCategories);
                _db.SaveChanges();
            }
        }
    }
}
