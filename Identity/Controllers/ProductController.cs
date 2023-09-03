using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping_List.Data;
using Shopping_List.Models;

namespace Shopping_List.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext context)
            {_context = context;}

        public IActionResult Index(string search , string category)
        {
            IQueryable <Product> product = _context.Product;
            if (!string.IsNullOrEmpty(search))
               product = product.Where(p => p.ProductTilte.Contains(search));
            else if(!string.IsNullOrEmpty(category))
               product = product.Where(p => p.ProductCategory == (category));
            return View(product.ToList());
        }

        public IActionResult List()
        {
            List<Product> products = _context.Product.ToList();
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            Product product = _context.Product.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                if (product.ProductCategory == "-1")
                {
                    ModelState.AddModelError("ProductCategory", "Please select a category.");
                    return View();
                }
                return View();
            }
            _context.Product.Update(product);
            _context.SaveChanges();
            if (ModelState.IsValid)
              return RedirectToAction("List");
            else
            { return View(); }
        }
        public IActionResult Delete(int id)
        {
            Product product = _context.Product.Find(id);
            _context.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                if (product.ProductCategory == "-1")
                {
                    ModelState.AddModelError("ProductCategory", "Please select a category.");
                    return View();
                }
                return View();
            }
            var titleExists = _context.Product.FirstOrDefault(x=>x.ProductTilte == product.ProductTilte);
            if (titleExists == null)
            { 
            _context.Product.Add(product);
            _context.SaveChanges();
            if (ModelState.IsValid)
                return RedirectToAction("List");
            else
            { return View(); }

            }
            if (product.ProductTilte == titleExists.ProductTilte)
            {
                ViewBag.Exists = "This product is already exists.";
                return View();
            }
            else
            { return View(); }
        }
    }
}
