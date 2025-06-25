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
    public class OrdersController : Controller
    {
        private readonly JopShopContext _context;

        public OrdersController(JopShopContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var jopShopContext = _context.Orders.Include(o => o.User);
            return View(await jopShopContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TotalPrice,Status,CreatedAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TotalPrice,Status,CreatedAt")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);
            if (user == null)
                return RedirectToAction("Login", "LoginAndRegistr");

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "CartItems");
            }

            var total = cartItems.Sum(item => item.Product.Price * item.Quantity);

            var order = new Order
            {
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                Status = "Pending",
                TotalPrice = total
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // لازم نحفظ عشان نحصل OrderId

            foreach (var item in cartItems)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product == null)
                {
                    TempData["Error"] = $"Product not found.";
                    return RedirectToAction("Index", "CartItems");
                }
                if (product.Quantity >= item.Quantity)
                {
                    product.Quantity = product.Quantity - item.Quantity;
                    _context.Products.Update(product);
                    _context.OrderItems.Add(new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price
                    });
                }
                else
                {
                    TempData["Error"] = $"Not enough stock for product: {product.Name}";
                    return RedirectToAction("Index", "CartItems");
                }
            }
            _context.CartItems.RemoveRange(cartItems); // فضي السلة
            await _context.SaveChangesAsync();
            TempData["Success"] = "Your order has been placed successfully!";
            return RedirectToAction("DeliveryForm", "Orders", new { id = order.Id });
        }
        public IActionResult DeliveryForm(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        public async Task<IActionResult> SellerOrders()
        {
            var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);

            if (seller == null)
                return RedirectToAction("Login", "Account");

            var orders = await _context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.User)
                .Include(oi => oi.Product)
                .Where(oi => oi.Product.SellerId == seller.Id)
                .ToListAsync();

            return View(orders);
        }



[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> formDelivery(int orderId, string FullName, string Address, string Phone)
{
    var order = await _context.Orders
        .Include(o => o.User)
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
        .FirstOrDefaultAsync(o => o.Id == orderId);

    if (order == null)
        return NotFound();

    // تنفيذ الشراء الفعلي الآن فقط
    foreach (var item in order.OrderItems)
    {
        if (item.Product != null && item.Product.Quantity >= item.Quantity)
        {
            item.Product.Quantity -= item.Quantity;
            _context.Products.Update(item.Product);
        }
        else
        {
            TempData["Error"] = $"Not enough stock for {item.Product?.Name ?? "product"}.";
            return RedirectToAction("DeliveryForm", new { id = orderId });
        }
    }
if (orderId == 0)
{
    TempData["Error"] = "Order not found!";
    return RedirectToAction("Index", "CartItems");
}

    // حذف من السلة (فقط العناصر اللي ضمن هذا الطلب)
    var userId = order.UserId;
    var productIdsInOrder = order.OrderItems.Select(oi => oi.ProductId).ToList();

    var cartItemsToRemove = await _context.CartItems
        .Where(ci => ci.UserId == userId && productIdsInOrder.Contains(ci.ProductId))
        .ToListAsync();

    _context.CartItems.RemoveRange(cartItemsToRemove);

    // حفظ معلومات التوصيل
    var delivery = new Delivery
    {
        OrderId = order.Id,
        Name = FullName,
        Address = Address,
        Phone = Phone,
        CreatedAt = DateTime.Now
    };
    _context.Deliveries.Add(delivery);

    // تحديث حالة الطلب
    order.Status = "Confirmed";
    _context.Orders.Update(order);

    await _context.SaveChangesAsync();

    TempData["Success"] = "Order placed successfully!";
    return RedirectToAction("Index", "Home");
}

    }
}