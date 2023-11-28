using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ICategoryService _categoryService;
        private IProductService _productService;

        public AdminController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<ActionResult> Index(int page = 1, int category = 0)
        {
            var products = await _productService.GetAllByCategory(category);

            int pageSize = 10;

            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentCategory = category,
                PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize)),
                PageSize = pageSize,
                CurrentPage = page
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new ProductAddViewModel();
            model.Product=new Product();
            model.Categories = await _categoryService.GetAll();
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddViewModel model)
        {
            await _productService.Add(model.Product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                TempData.Add("message", $"Your product deleted successfully");
                    
            }
            catch (Exception ex)
            {
                TempData.Add("message", $"Not Found");
            }
                return RedirectToAction("Index");
        }

    }
}
