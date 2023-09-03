using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_List.Data;
using Shopping_List.Models;

namespace Shopping_List.Controllers
{
    public class ListController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly ProductDbContext _context;
        public ListController(ProductDbContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ShoppingList shoppingList)
        {
            //ShoppingList list = new ShoppingList();
            ShoppingListDto list = new ShoppingListDto();
            ListDetails details = new ListDetails();
            ListHeaders listHeaders = new ListHeaders ();
            
            string userId = _userManager.GetUserId(User);

            //bool userExists = _context.ListHeaders.Any(x => x.UserId == userId);
            //if (userExists == false)
            //{
            //    ListHeaders headers = new ListHeaders { UserId = userId };
            //    _context.ListHeaders.Add(headers);
            //    _context.SaveChanges();
            //}
            //ListHeaders listHeaders = _context.ListHeaders.AsNoTracking().FirstOrDefault(x=>x.UserId == list.ListHeaders.UserId);

            var userProductHeader = _context.ListHeaders.FirstOrDefault(ph => ph.UserId == userId);
            if (userProductHeader == null)
            {
                ListHeaders headers = new ListHeaders { UserId = userId };
                _context.ListHeaders.Add(headers);
                _context.SaveChanges();
                var userProductHeader2 = _context.ListHeaders.FirstOrDefault(ph => ph.UserId == userId);
                userProductHeader = userProductHeader2;
            }
            ShoppingList shoppingList1 = new ShoppingList()
            {
                ListId = shoppingList.ListId,
                HeaderId = userProductHeader.HeaderId,
                ListTitle = shoppingList.ListTitle,
                Status = "Edit"
            };
            _context.ShoppingLists.Add(shoppingList1);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var userId = _userManager.GetUserId(User);
            var userProductHeader = _context.ListHeaders.FirstOrDefault(ph => ph.UserId == userId);
            if (userProductHeader == null)
            {
                if (userId != null)
                {
                    var newUserProductHeader = new ListHeaders
                    {
                        UserId = userId,
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

            List<ShoppingList> shoppingLists = _context.ShoppingLists.Where(x => x.HeaderId == userProductHeader.HeaderId).ToList();
            return View(shoppingLists);
        }
        public IActionResult Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var listDetails = _context.ListDetails.FirstOrDefault(f=>f.ListId == id);
            if (listDetails == null)
            {
                return View();
            }
            ListHeaders listHeaders = _context.ListHeaders.Find(listDetails.HeaderId);
            if (listHeaders.UserId != userId)
            {
                return RedirectToAction("List");
            }
            List<ShoppingList> shoppingList = _context.ShoppingLists.ToList();
            ShoppingList shoppingList2 = new ShoppingList();
            List<ListDetails> listDetails2 = _context.ListDetails.Where(x => x.ListId == listDetails.ListId).ToList();
            List<ListDetails> listDetails3 = _context.ListDetails.Where(x => x.HeaderId == listHeaders.HeaderId).ToList();

            var shoppingListDto = new ListDto
            {
                listDetails = listDetails2,
                ShoppingList = shoppingList
            };


            //foreach (var item in listDetails2)
            //{
            //var product = _context.Product.FirstOrDefault(p=>p.ProductId == item.ProductId);

            //string productImage = product?.ImageUrl;

            //ViewBag.ProductImage = productImage;
            //}
            ViewBag.CurrentPageId = id;
            return View(shoppingListDto);
        }
        [HttpPost]
        public IActionResult Details2(int currentPageId)
        {
            //ShoppingList shoppingList = new ShoppingList();
            ShoppingList shoppingList = _context.ShoppingLists.FirstOrDefault(x => x.ListId == currentPageId);
            if (shoppingList.Status == "Edit")
            {
                shoppingList.Status = "Shopping";
                _context.SaveChanges();
            return RedirectToAction("Details",new {id = currentPageId});
            }
            if (shoppingList.Status != "Edit")
            {
                shoppingList.Status = "Edit";
                _context.SaveChanges();
            return RedirectToAction("Details",new {id = currentPageId});
            }

            return RedirectToAction("Details",new {id = currentPageId});
        }

        public IActionResult DeleteProduct(int id)
        {
            ListDetails listDetails = _context.ListDetails.Find(id);
            _context.Remove(listDetails);
            _context.SaveChanges();
            return RedirectToAction("Details", new {id = listDetails.ListId});
        }

        public IActionResult DeleteList(int id)
        {
            ShoppingList shoppingList = _context.ShoppingLists.Find(id);
            _context.Remove(shoppingList);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult EditDescription(int id)
        {
            var listDetails = _context.ListDetails.FirstOrDefault(f=>f.DetailsId == id);
            return View(listDetails);
        }

        [HttpPost]
        public IActionResult EditDescription(ListDetails listDetails)
        {
            var listDetail = _context.ListDetails.FirstOrDefault(x=>x.DetailsId == listDetails.DetailsId);
            listDetail.Description = listDetails.Description;
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = listDetails.ListId });
        }
    }
}
