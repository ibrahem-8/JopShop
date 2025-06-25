using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JopShop.Models;

namespace JopShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly JopShopContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProductsController(JopShopContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // ✅ عرض المنتجات غير المخفية والتي فيها كمية فقط
        public async Task<IActionResult> Index()
        {
            var jopShopContext = _context.Products
                .Include(p => p.Seller)
                .Where(p => !p.IsHidden && p.Quantity > 0);

            return View(await jopShopContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            //ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Id");
            var currentEmail = HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(currentEmail))
                return RedirectToAction("Login", "LoginAndRegistr");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Category,Price,Quantity,ImageFile")] Product product)
        {
            string? currentUserEmail = HttpContext.Session.GetString("CurrentUser");
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);

            if (seller == null)
                return RedirectToAction("Login", "Account");

            product.SellerId = seller.Id;
            product.CreatedAt = DateTime.Now;

            if (product.ImageFile != null)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                string imageName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/images/", imageName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }
                product.ImageUrl = imageName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Id", product.SellerId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);

            if (seller == null || product.SellerId != seller.Id)
                return Forbid();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Category,Price,Quantity,ImageUrl,SellerId,CreatedAt,IsHidden")] Product product, IFormFile? ImageFile)
        {
            if (id != product.Id)
                return NotFound();

            var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);

            if (seller == null || product.SellerId != seller.Id)
                return Forbid();

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string imageName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/images/", imageName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }
                        product.ImageUrl = imageName;
                    }

                    // ✅ إظهار المنتج تلقائياً إذا زادت الكمية عن 0
                    if (product.Quantity > 0)
                    {
                        product.IsHidden = false;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);

            if (seller == null || product.SellerId != seller.Id)
                return Forbid();

            return View(product);
        }

        // ✅ حذف أو إخفاء المنتج بناءً على وجود طلبات
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            bool hasOrders = _context.OrderItems.Any(o => o.ProductId == id);

            if (hasOrders)
            {
                product.IsHidden = true;
                TempData["Message"] = "This product has orders and was hidden instead of deleted.";
            }
            else
            {
                _context.Products.Remove(product);
                TempData["Message"] = "Product deleted successfully.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}









