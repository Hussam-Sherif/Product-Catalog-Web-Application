namespace Product_Catalog_Web_Application.Core.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();

        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
    }
}
