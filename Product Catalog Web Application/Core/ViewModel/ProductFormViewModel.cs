using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Catalog_Web_Application.Core.Models;

namespace Product_Catalog_Web_Application.Core.ViewModel
{
    public class ProductFormViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        //public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public List<int> SelectedCategoryIds { get; set; } = null!;

    }
}
