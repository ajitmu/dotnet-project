using Microsoft.AspNetCore.Mvc;
using product.Models;

namespace product.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Wireless Gaming Mouse", Category = "Electronics", Price = 1499.00m, Description = "High precision wireless gaming mouse with RGB.", IsInStock = true, ImageUrl = "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=400" },
            new Product { Id = 2, Name = "Mechanical Keyboard", Category = "Electronics", Price = 3499.00m, Description = "Tactile mechanical keyboard with customizable keys.", IsInStock = true, ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=400" },
            new Product { Id = 3, Name = "Noise Cancelling Headphones", Category = "Audio", Price = 8999.00m, Description = "Premium wireless headphones with active noise cancelling.", IsInStock = false, ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400" },
            new Product { Id = 4, Name = "Smart Watch Series 7", Category = "Wearables", Price = 12999.00m, Description = "Fitness tracker, heart rate monitor and AMOLED display.", IsInStock = true, ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=400" }
        };

        public IActionResult Index(string searchString, string category)
        {
            var products = _products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || p.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                products = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            ViewBag.Categories = _products.Select(p => p.Category).Distinct().ToList();
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = category;

            return View(products.ToList());
        }

        public IActionResult Details(int id)
        {
            var p = _products.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                p.Id = _products.Count > 0 ? _products.Max(x => x.Id) + 1 : 1;
                if (string.IsNullOrEmpty(p.ImageUrl))
                {
                    p.ImageUrl = "https://via.placeholder.com/300x200?text=No+Image";
                }
                _products.Add(p);
                return RedirectToAction(nameof(Index));
            }
            return View(p);
        }
    }
}