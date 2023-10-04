using Product_Catalog_Web_Application.Core.Models;

namespace Product_Catalog_Web_Application.Core.ViewModel
{
    public class ProductVM
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public List<string> Categories { get; set; } 

    }
}
