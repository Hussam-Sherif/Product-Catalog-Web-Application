namespace Product_Catalog_Web_Application.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ProductCategory> Products { get; set; } = new List<ProductCategory>();

    }
}
