using Shopping_List.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Shopping_List.Data;
using Microsoft.AspNetCore.Identity;

namespace Shopping_List.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly ProductDbContext _context;
        public HomeController(ProductDbContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    List<Product> products = _context.Product.ToList();
        //    return View(products);
        //}

        public IActionResult Index(string search, string category)
        {
            IQueryable<Product> product = _context.Product;
            if (!string.IsNullOrEmpty(search))
                product = product.Where(p => p.ProductTilte.Contains(search));
            else if (!string.IsNullOrEmpty(category))
                product = product.Where(p => p.ProductCategory == (category));
            return View(product.ToList());
        }

        public IActionResult Details(int id)
        {
            ListDetails listDetails = new ListDetails();
            Product product1 = new Product();
            var userId = _userManager.GetUserId(User);
            var userProductHeader = _context.ListHeaders.FirstOrDefault(ph => ph.UserId == userId);
            if (userProductHeader==null)
            {
                if (userId != null)
            {
                var newUserProductHeader = new ListHeaders
                {
                    UserId = userId,
                    // Diğer gerekli alanları da ekleyebilirsiniz
                };
                    _context.ListHeaders.Add(newUserProductHeader);
                    _context.SaveChanges();
                    userProductHeader = newUserProductHeader;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            List<ShoppingList> shoppingList = _context.ShoppingLists.Where(x => x.HeaderId == userProductHeader.HeaderId).ToList();

            //List<ShoppingList> shoppingList = _context.ShoppingLists.ToList();

            Product product = _context.Product.Find(id);
            var shoppingListDto = new ShoppingListDto
            {
                product = product,
                ShoppingList = shoppingList
            };
            shoppingListDto.listDetails2 = _context.ListDetails
    .Where(ld => ld.ListId == id)
    .ToList();

            //var filteredListDetails = shoppingListDto.listDetails
            //.Where(ld => ld.ListId == ViewBag.CurrentPageId);

            //ShoppingListDto shoppingListDto = new ShoppingListDto();
            //var shoppingListDto = new ShoppingListDto();
            //shoppingListDto.Product = _context.Product.Find(id);
            ViewBag.CurrentPageId = id;

            return View(shoppingListDto);
        }

        [HttpPost]
        public IActionResult Details(int currentPageId,ListDetails listDetails)
        {
            var userId = _userManager.GetUserId(User);
            var userProductHeader = _context.ListHeaders.FirstOrDefault(ph => ph.UserId == userId);
            ListDetails listDetails1 = new ListDetails()
            {
                HeaderId = userProductHeader.HeaderId,
                ListId = listDetails.ListId,
                ProductTitle = listDetails.ProductTitle,
                ProductId = listDetails.ProductId,
                Description = listDetails.Description,
                ImageUrl = listDetails.ImageUrl
            };

            if (listDetails1.ListId == -1 || listDetails1.ListId == 0)
            {
                ViewBag.ListIdError= "Please choose a list.";
                return RedirectToAction("Details", new { id = currentPageId });    
            }
            //int productIdToAdd = listDetails1.ProductId;
            //int listIdToAdd = listDetails1.ListId;

            //var existListId = _context.ListDetails
            //    .Where(p => p.ProductId == productIdToAdd)
            //    .Select(s => s.ListId)
            //    .ToList();

            //if (existListId.Contains(listIdToAdd))
            //{
            //    ViewBag.ErrorMessage = "Seçtiğin listede bu ürün zaten mevcut!";
            //    return View();
            //}
            //else {
            //    _context.ListDetails.Add(listDetails1);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            _context.ListDetails.Add(listDetails1);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}