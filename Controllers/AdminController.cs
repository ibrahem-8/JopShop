using Microsoft.AspNetCore.Mvc;
using JopShop.Models;
using JopShop.ViewModels;
using System.Linq;

namespace JopShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly JopShopContext _context;

        public AdminController(JopShopContext context)
        {
            _context = context;
        }

        //         public IActionResult Index()
        //         {
        //       var viewModel = new AdminDashboardViewModel
        // {
        //     TotalProducts = _context.Products.Count(),
        //     TotalProjects = _context.Jobs.Count(), // ← استبدال Projects بـ Jobs
        //     TotalUsers = _context.Users.Count(),
        //     TotalOrders = _context.Orders.Count(),
        //     TotalPosts = _context.Posts.Count(),

        //     LatestProducts = _context.Products.OrderByDescending(p => p.Id).Take(5).ToList(),
        //     LatestJobs = _context.Jobs.OrderByDescending(j => j.Id).Take(5).ToList(), // ← بدل LatestProjects
        //     LatestUsers = _context.Users.OrderByDescending(u => u.Id).Take(5).ToList(),
        //     LatestPosts = _context.Posts.OrderByDescending(p => p.Id).Take(5).ToList(),

        //     OrderStatusDistribution = _context.Orders
        //         .GroupBy(o => o.Status ?? "Unknown")
        //         .ToDictionary(g => g.Key, g => g.Count())
        // };


        //             return View(viewModel);
        //         }



        // public IActionResult Index(string search)
        // {
        //     var products = _context.Products.AsQueryable();

        //     if (!string.IsNullOrEmpty(search))
        //         products = products.Where(p => p.Name.Contains(search));

        //     var viewModel = new AdminDashboardViewModel
        //     {
        //         TotalProducts = _context.Products.Count(),
        //         TotalProjects = _context.Jobs.Count(),
        //         TotalUsers = _context.Users.Count(),
        //         TotalOrders = _context.Orders.Count(),
        //         TotalPosts = _context.Posts.Count(),
        // AllProducts = products.OrderByDescending(p => p.Id).ToList(),

        //         LatestProducts = products.OrderByDescending(p => p.Id).ToList(), // عرض الكل مش آخر 5
        //         LatestJobs = _context.Jobs.OrderByDescending(j => j.Id).Take(5).ToList(),
        //         LatestUsers = _context.Users.OrderByDescending(u => u.Id).Take(5).ToList(),
        //         LatestPosts = _context.Posts.OrderByDescending(p => p.Id).Take(5).ToList(),

        //         OrderStatusDistribution = _context.Orders
        //             .GroupBy(o => o.Status ?? "Unknown")
        //             .ToDictionary(g => g.Key, g => g.Count())
        //     };

        //     return View(viewModel);
        // }

        public IActionResult Index(string search)
        {
            // ✅ فلترة المنتجات التي لم يتم إخفاؤها والتي فيها كمية
            var products = _context.Products
                .Where(p => !p.IsHidden && p.Quantity > 0)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                products = products.Where(p => p.Name.Contains(search));
    // ✅ إجمالي المبيعات للطلبات المكتملة فقط
    var totalSales = _context.Orders
        .Where(o => o.Status == "Delivered")
        .Sum(o => (int?)o.TotalPrice) ?? 0;

    // ✅ المبيعات الشهرية للسنة الحالية
  var monthlySalesRaw = _context.Orders
    .Where(o => o.Status == "Delivered" && o.CreatedAt.HasValue && o.CreatedAt.Value.Year == DateTime.Now.Year)
    .GroupBy(o => o.CreatedAt.Value.Month)
    .ToDictionary(
        g => g.Key,
        g => (int)(g.Sum(o => o.TotalPrice) ?? 0)
    );

var monthlySales = Enumerable.Range(1, 12)
    .ToDictionary(
        month => new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM"),
        month => monthlySalesRaw.ContainsKey(month) ? monthlySalesRaw[month] : 0
    );


            var viewModel = new AdminDashboardViewModel
            {


                TotalProducts = _context.Products.Count(),
                TotalProjects = _context.Jobs.Count(),
                TotalUsers = _context.Users.Count(),
                TotalOrders = _context.Orders.Count(),
                TotalPosts = _context.Posts.Count(),

                AllProducts = products.OrderByDescending(p => p.Id).ToList(),
                LatestProducts = products.OrderByDescending(p => p.Id).ToList(),
                LatestJobs = _context.Jobs.OrderByDescending(j => j.Id).Take(5).ToList(),
                LatestUsers = _context.Users.OrderByDescending(u => u.Id).Take(5).ToList(),
                AllUsers = _context.Users.ToList(),
                LatestPosts = _context.Posts.OrderByDescending(p => p.Id).Take(5).ToList(),

                OrderStatusDistribution = _context.Orders
                    .GroupBy(o => o.Status ?? "Unknown")
                    .ToDictionary(g => g.Key, g => g.Count()),
                    
    TotalSales = totalSales,
    MonthlySales = monthlySales

                    
            };

            return View(viewModel);
        }


        //         [HttpPost]
        // // public async Task<IActionResult> DeleteProduct(int id)
        // // {
        // //     var product = await _context.Products.FindAsync(id);
        // //     if (product != null)
        // //     {
        // //         _context.Products.Remove(product);
        // //         await _context.SaveChangesAsync();
        // //     }
        // //     return RedirectToAction("Products");
        // // }
        // [HttpPost]
        // public async Task<IActionResult> DeleteProduct(int id)
        // {
        //     var product = await _context.Products.FindAsync(id);
        //     if (product != null)
        //     {
        //         // حذف من السلال فقط
        //         var cartItems = _context.CartItems.Where(ci => ci.ProductId == id);
        //         _context.CartItems.RemoveRange(cartItems);

        //         // حذف المنتج
        //         _context.Products.Remove(product);

        //         await _context.SaveChangesAsync();
        //     }

        //     return RedirectToAction("Products");
        // }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                bool hasOrders = _context.OrderItems.Any(o => o.ProductId == id);

                // حذف من السلال أولاً
                var cartItems = _context.CartItems.Where(ci => ci.ProductId == id);
                _context.CartItems.RemoveRange(cartItems);

                if (hasOrders)
                {
                    // ✅ إخفاء بدل الحذف
                    product.IsHidden = true;
                    TempData["Message"] = "Product was hidden due to existing orders.";
                }
                else
                {
                    _context.Products.Remove(product);
                    TempData["Message"] = "Product deleted successfully.";
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index"); // أو Products
        }

        [HttpPost]
        public async Task<IActionResult> ToggleSuspend(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Suspended = !(user.Suspended ?? false);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePublish(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.IsPublished = !user.IsPublished;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

// [HttpPost]
// public async Task<IActionResult> DeleteUser(int id)
// {
//     var user = await _context.Users.FindAsync(id);
//     if (user == null) return NotFound();

//     // حذف التعليقات
//     var comments = _context.Comments.Where(c => c.UserId == id);
//     _context.Comments.RemoveRange(comments);

//     // حذف الطلبات والـ Deliveries
//     var orders = _context.Orders.Where(o => o.UserId == id).ToList();
//     var orderIds = orders.Select(o => o.Id).ToList();

//     var deliveries = _context.Deliveries.Where(d => orderIds.Contains(d.OrderId));
//     _context.Deliveries.RemoveRange(deliveries);

// var orderItems = _context.OrderItems
//     .Where(oi => oi.OrderId.HasValue && orderIds.Contains(oi.OrderId.Value));



//     _context.Orders.RemoveRange(orders);

//     // حذف عناصر السلة
//     var cartItems = _context.CartItems.Where(c => c.UserId == id);
//     _context.CartItems.RemoveRange(cartItems);
//     // حذف المنتجات
//     var products = _context.Products.Where(p => p.SellerId == id);
//     _context.Products.RemoveRange(products);
//   // حذف الطلبات على الوظائف
//     var applications = _context.Applications.Where(a => a.UserId == id);
//     _context.Applications.RemoveRange(applications);
//     // حذف الوظائف
//     var jobs = _context.Jobs.Where(j => j.EmployerId == id);
//     _context.Jobs.RemoveRange(jobs);

  


//     // حذف التقييمات
//     var ratings = _context.Ratings.Where(r => r.RatedUserId == id || r.FreelancerId == id);
//     _context.Ratings.RemoveRange(ratings);

//     // حذف المستخدم نفسه
//     _context.Users.Remove(user);

//     await _context.SaveChangesAsync();
//     return RedirectToAction("Index");
// }

[HttpPost]
public async Task<IActionResult> DeleteUser(int id)
{
    var user = await _context.Users.FindAsync(id);
    if (user == null) return NotFound();

    // حذف التعليقات
    var comments = _context.Comments.Where(c => c.UserId == id);
    _context.Comments.RemoveRange(comments);

    // حذف الطلبات
    var orders = _context.Orders.Where(o => o.UserId == id).ToList();
    var orderIds = orders.Select(o => o.Id).ToList();

    var deliveries = _context.Deliveries.Where(d => orderIds.Contains(d.OrderId));
    _context.Deliveries.RemoveRange(deliveries);

    var orderItems = _context.OrderItems
        .Where(oi => oi.OrderId.HasValue && orderIds.Contains(oi.OrderId.Value));
    _context.OrderItems.RemoveRange(orderItems);

    _context.Orders.RemoveRange(orders);

    // حذف التقديمات على الوظائف المرتبطة بالوظائف اللي أنشأها المستخدم
    var jobIds = _context.Jobs.Where(j => j.EmployerId == id).Select(j => j.Id).ToList();
   var applicationsForJobs = _context.Applications
    .Where(a => a.JobId.HasValue && jobIds.Contains(a.JobId.Value));

    _context.Applications.RemoveRange(applicationsForJobs);

    // حذف الطلبات على الوظائف اللي قدّم عليها المستخدم
    var applications = _context.Applications.Where(a => a.UserId == id);
    _context.Applications.RemoveRange(applications);

    // حذف الوظائف
    var jobs = _context.Jobs.Where(j => j.EmployerId == id);
    _context.Jobs.RemoveRange(jobs);

    // حذف المنتجات
  var products = _context.Products.Where(p => p.SellerId == id).ToList();
var productIds = products.Select(p => p.Id).ToList();

var orderItemsForProducts = _context.OrderItems
    .Where(oi => oi.ProductId.HasValue && productIds.Contains(oi.ProductId.Value));
_context.OrderItems.RemoveRange(orderItemsForProducts);

_context.Products.RemoveRange(products);


    // حذف عناصر السلة
    var cartItems = _context.CartItems.Where(c => c.UserId == id);
    _context.CartItems.RemoveRange(cartItems);

    // حذف التقييمات
    var ratings = _context.Ratings.Where(r => r.RatedUserId == id || r.FreelancerId == id);
    _context.Ratings.RemoveRange(ratings);

    // حذف المستخدم نفسه
    _context.Users.Remove(user);

    await _context.SaveChangesAsync();
    return RedirectToAction("Index");
}

    }
}
