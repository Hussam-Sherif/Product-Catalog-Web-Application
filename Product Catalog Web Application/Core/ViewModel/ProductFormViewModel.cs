using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Catalog_Web_Application.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Product_Catalog_Web_Application.Core.Const;

namespace Product_Catalog_Web_Application.Core.ViewModel
{
    public class ProductFormViewModel
    {
        public int ID { get; set; }
        [Required, MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Product Name")]
        [Remote("AllowItem", "Product", AdditionalFields = "ID", ErrorMessage = Errors.Duplicate)]
        [RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public string? CreatedBy { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public List<int> SelectedCategoryIds { get; set; } = null!;

    }
}
