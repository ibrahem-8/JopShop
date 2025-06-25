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
    public class OrderItemsController : Controller
    {
        private readonly JopShopContext _context;

        public OrderItemsController(JopShopContext context)
        {
            _context = context;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var jopShopContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            return View(await jopShopContext.ToListAsync());
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrderItems/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,Quantity,Price")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId,Quantity,Price")] OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> MarkAsDelivered(int orderItemId)
{
    var orderItem = await _context.OrderItems
        .Include(o => o.Order)
        .FirstOrDefaultAsync(o => o.Id == orderItemId);

    if (orderItem == null)
        return NotFound();

    orderItem.Order.Status = "Delivered";
    await _context.SaveChangesAsync();

    TempData["Success"] = "تم تحديث حالة الطلب إلى تم التوصيل.";
    return RedirectToAction("SellerOrders", "Orders");
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed2(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item != null)
            {
                _context.OrderItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Order deleted successfully!";
            return RedirectToAction("Index", "Orders");

        }
public async Task<IActionResult> MyOrders()
{
    var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);
    if (user == null) return RedirectToAction("Login", "LoginAndRegistr");

    var orders = await _context.OrderItems
        .Include(oi => oi.Product)
        .Include(oi => oi.Order)
        .Where(oi => oi.Order.UserId == user.Id)
        .ToListAsync();

    return View(orders);
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed3(int id)
{
    var orderItem = await _context.OrderItems.FindAsync(id);
    if (orderItem == null)
        return NotFound();

    _context.OrderItems.Remove(orderItem);
    await _context.SaveChangesAsync();

    return RedirectToAction("MyOrders");
}

[HttpPost]
public async Task<IActionResult> DeleteAllConfirmed()
{
    var orders = await _context.OrderItems.ToListAsync();
    _context.OrderItems.RemoveRange(orders);
    await _context.SaveChangesAsync();
    return RedirectToAction("MyOrders");
}

    }
}
