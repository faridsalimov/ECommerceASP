using ECommerce.Entities.Models;

namespace ECommerce.WebUI.Models
{
    public class ProductListViewModel
    {
        public List<Product>? Products { get; set; }
        public int CurrentCategory { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool hasSortClicked { get; set; }
        public bool hasDescSortClicked { get; set; }
        public bool hasSortPrice { get; set; }
        public bool hasDescSortPrice { get; set; }
    }
}