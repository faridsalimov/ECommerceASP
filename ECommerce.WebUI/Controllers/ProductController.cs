using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{

    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
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

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> SortWithLetter(bool hasSortClicked, int page = 1, int category = 0)
        {
            if (hasSortClicked != true)
            {
                var products = await _productService.GetAllByCategory(category);
                var sortedProducts = products.OrderBy(p => p.ProductName);
                int pageSize = 10;
                var model = new ProductListViewModel();
                model.Products = sortedProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                model.CurrentCategory = category;
                model.PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize));
                model.PageSize = pageSize;
                model.CurrentPage = page;
                model.hasSortClicked = true;
                return View("Index", model);
            }
			
            else
            {
                var products = await _productService.GetAllByCategory(category);
                var sortedProducts = products.OrderByDescending(p => p.ProductName);
                int pageSize = 10;
                var model = new ProductListViewModel();
                model.Products = sortedProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                model.CurrentCategory = category;
                model.PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize));
                model.PageSize = pageSize;
                model.CurrentPage = page;
                model.hasSortClicked = false;
                return View("Index", model);
            }
        }

        public async Task<IActionResult> SortForPrice(int page = 1, int category = 0)
        {
            var products = await _productService.GetAllByCategory(category);
            var sortedProducts = products.OrderBy(p => p.UnitPrice);
            int pageSize = 10;
            var model = new ProductListViewModel();
            model.Products = sortedProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            model.CurrentCategory = category;
            model.PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize));
            model.PageSize = pageSize;
            model.CurrentPage = page;
            model.hasSortPrice = true;
            return View("Index", model);
        }
		
        public async Task<IActionResult> SortForPriceDesc(int page = 1, int category = 0)
        {
            var products = await _productService.GetAllByCategory(category);
            var sortedProducts = products.OrderByDescending(p => p.UnitPrice);
            int pageSize = 10;
            var model = new ProductListViewModel();
            model.Products = sortedProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            model.CurrentCategory = category;
            model.PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize));
            model.PageSize = pageSize;
            model.CurrentPage = page;
            model.hasDescSortPrice = true;
            return View("Index", model);
        }

        public async Task<List<Product>> Search(string word)
        {
            var allProducts = await _productService.GetAll();

            if (allProducts != null && !string.IsNullOrEmpty(word))
            {
                var result = allProducts.Where(r => r.ProductName.ToLower().Contains(word.ToLower())).ToList();
                var model = new ProductListViewModel
                {
                    Products = result
                };
                return result;
            }
			
            return null;
        }
    }
}
