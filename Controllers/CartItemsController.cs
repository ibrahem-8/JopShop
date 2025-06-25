// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using JopShop.Models;

// namespace JopShop.Controllers
// {
//     public class CartItemsController : Controller
//     {
//         private readonly JopShopContext _context;

//         public CartItemsController(JopShopContext context)
//         {
//             _context = context;
//         }

//         // GET: CartItems
//         public async Task<IActionResult> Index()
//         {
//             var jopShopContext = _context.CartItems.Include(c => c.Product).Include(c => c.User);
//             return View(await jopShopContext.ToListAsync());
//         }

//         // GET: CartItems/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var cartItem = await _context.CartItems
//                 .Include(c => c.Product)
//                 .Include(c => c.User)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (cartItem == null)
//             {
//                 return NotFound();
//             }

//             return View(cartItem);
//         }

//         // GET: CartItems/Create
//         public IActionResult Create()
//         {
//             ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
//             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
//             return View();
//         }

//         // POST: CartItems/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Id,UserId,ProductId,Quantity,AddedAt")] CartItem cartItem)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(cartItem);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartItem.ProductId);
//             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cartItem.UserId);
//             return View(cartItem);
//         }

//         // GET: CartItems/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var cartItem = await _context.CartItems.FindAsync(id);
//             if (cartItem == null)
//             {
//                 return NotFound();
//             }
//             ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartItem.ProductId);
//             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cartItem.UserId);
//             return View(cartItem);
//         }

//         // POST: CartItems/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProductId,Quantity,AddedAt")] CartItem cartItem)
//         {
//             if (id != cartItem.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(cartItem);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!CartItemExists(cartItem.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartItem.ProductId);
//             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cartItem.UserId);
//             return View(cartItem);
//         }

//         // GET: CartItems/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var cartItem = await _context.CartItems
//                 .Include(c => c.Product)
//                 .Include(c => c.User)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (cartItem == null)
//             {
//                 return NotFound();
//             }

//             return View(cartItem);
//         }

//         // POST: CartItems/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var cartItem = await _context.CartItems.FindAsync(id);
//             if (cartItem != null)
//             {
//                 _context.CartItems.Remove(cartItem);
//             }

//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool CartItemExists(int id)
//         {
//             return _context.CartItems.Any(e => e.Id == id);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> AddToCart(int productId)
//         {
//             var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
//             var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);
//             if (user == null) return RedirectToAction("Login", "Account");

//             var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
//             if (product == null || product.Quantity == 0) return NotFound();

//             var existingCartItem = await _context.CartItems
//                 .FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);

//             if (existingCartItem != null)
//             {
//                 if (existingCartItem.Quantity + 1 > product.Quantity)
//                 {
//                     TempData["Error"] = "Not enough quantity in stock.";
//                     return RedirectToAction("Index", "Products");
//                 }

//                 existingCartItem.Quantity += 1;
//                 _context.Update(existingCartItem);
//             }
//             else
//             {
//                 var cartItem = new CartItem
//                 {
//                     UserId = user.Id,
//                     ProductId = productId,
//                     Quantity = 1,
//                     AddedAt = DateTime.Now
//                 };
//                 _context.CartItems.Add(cartItem);
//             }

//             await _context.SaveChangesAsync();
//             return RedirectToAction("Index", "Products");
//         }
// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> IncreaseQuantity(int cartItemId)
// {
//     var cartItem = await _context.CartItems
//         .Include(c => c.Product)
//         .FirstOrDefaultAsync(c => c.Id == cartItemId);

//     if (cartItem == null) return NotFound();

//     if (cartItem.Quantity < cartItem.Product.Quantity)
//     {
//         cartItem.Quantity += 1;
//         _context.Update(cartItem);
//         await _context.SaveChangesAsync();
//     }

//     return RedirectToAction(nameof(Index));
// }

// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> DecreaseQuantity(int cartItemId)
// {
//     var cartItem = await _context.CartItems.FindAsync(cartItemId);
//     if (cartItem == null) return NotFound();

//     if (cartItem.Quantity > 1)
//     {
//         cartItem.Quantity -= 1;
//         _context.Update(cartItem);
//         await _context.SaveChangesAsync();
//     }

//     return RedirectToAction(nameof(Index));
// }



//         [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> CheckoutSingleItem(int cartItemId)
// {
//     var cartItem = await _context.CartItems
//         .Include(c => c.Product)
//         .Include(c => c.User)
//         .FirstOrDefaultAsync(c => c.Id == cartItemId);

//     if (cartItem == null || cartItem.Product == null || cartItem.User == null)
//     {
//         return NotFound();
//     }

//     if (cartItem.Quantity > cartItem.Product.Quantity)
//     {
//         TempData["Error"] = "Not enough quantity in stock.";
//         return RedirectToAction("Index", "CartItems");
//     }

//     // إنشاء الطلب
//     var order = new Order
//     {
//         UserId = cartItem.UserId,
//         CreatedAt = DateTime.Now,
//         Status = "Pending"
//     };
//     _context.Orders.Add(order);
//     await _context.SaveChangesAsync();

//     // إضافة عنصر الطلب
//     var orderItem = new OrderItem
//     {
//         OrderId = order.Id,
//         ProductId = cartItem.ProductId,
//         Quantity = cartItem.Quantity,
//         Price = cartItem.Product.Price
//     };
//     _context.OrderItems.Add(orderItem);

//     // خصم الكمية من المنتج
//     cartItem.Product.Quantity -= cartItem.Quantity;
//     _context.Products.Update(cartItem.Product);

//     // حذف من السلة
//     _context.CartItems.Remove(cartItem);

//     await _context.SaveChangesAsync();

// return RedirectToAction("DeliveryForm", "Orders", new { orderId = order.Id });


// }

//     }
// }
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
    public class CartItemsController : Controller
    {
        private readonly JopShopContext _context;

        public CartItemsController(JopShopContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var jopShopContext = _context.CartItems.Include(c => c.Product).Include(c => c.User);
            return View(await jopShopContext.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProductId,Quantity,AddedAt")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartItem.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cartItem.UserId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartItem.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cartItem.UserId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProductId,Quantity,AddedAt")] CartItem cartItem)
        {
            if (id != cartItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cartItem.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cartItem.UserId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);
            if (user == null) return RedirectToAction("Login", "Account");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null || product.Quantity == 0) return NotFound();

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);

            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity + 1 > product.Quantity)
                {
                    TempData["Error"] = "Not enough quantity in stock.";
                    return RedirectToAction("Index", "Products");
                }

                existingCartItem.Quantity += 1;
                _context.Update(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = user.Id,
                    ProductId = productId,
                    Quantity = 1,
                    AddedAt = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Products");
        }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> IncreaseQuantity(int cartItemId)
{
    var cartItem = await _context.CartItems
        .Include(c => c.Product)
        .FirstOrDefaultAsync(c => c.Id == cartItemId);

    if (cartItem == null) return NotFound();

    if (cartItem.Quantity < cartItem.Product.Quantity)
    {
        cartItem.Quantity += 1;
        _context.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DecreaseQuantity(int cartItemId)
{
    var cartItem = await _context.CartItems.FindAsync(cartItemId);
    if (cartItem == null) return NotFound();

    if (cartItem.Quantity > 1)
    {
        cartItem.Quantity -= 1;
        _context.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
}

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> CheckoutSingleItem(int cartItemId)
//         {
//             var cartItem = await _context.CartItems
//                 .Include(c => c.Product)
//                 .Include(c => c.User)
//                 .FirstOrDefaultAsync(c => c.Id == cartItemId);

//             if (cartItem == null || cartItem.Product == null || cartItem.User == null)
//             {
//                 return NotFound();
//             }

//             if (cartItem.Quantity > cartItem.Product.Quantity)
//             {
//                 TempData["Error"] = "Not enough quantity in stock.";
//                 return RedirectToAction("Index", "CartItems");
//             }

//             // إنشاء الطلب
//             var order = new Order
//             {
//                 UserId = cartItem.UserId,
//                 CreatedAt = DateTime.Now,
//                 Status = "Pending"
//             };
//             _context.Orders.Add(order);
//             await _context.SaveChangesAsync();

//             // إضافة عنصر الطلب
//             var orderItem = new OrderItem
//             {
//                 OrderId = order.Id,
//                 ProductId = cartItem.ProductId,
//                 Quantity = cartItem.Quantity,
//                 Price = cartItem.Product.Price
//             };
//             _context.OrderItems.Add(orderItem);

//             // خصم الكمية من المنتج
//             cartItem.Product.Quantity -= cartItem.Quantity;
//             _context.Products.Update(cartItem.Product);

//             // حذف من السلة
//             _context.CartItems.Remove(cartItem);

//             await _context.SaveChangesAsync();

//             // return RedirectToAction("Details", "Orders");

// return RedirectToAction("DeliveryForm", "Orders", new { id = order.Id });
//         }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> CheckoutSingleItem(int cartItemId)
{
    var cartItem = await _context.CartItems
        .Include(c => c.Product)
        .Include(c => c.User)
        .FirstOrDefaultAsync(c => c.Id == cartItemId);

    if (cartItem == null || cartItem.Product == null || cartItem.User == null)
        return NotFound();

    if (cartItem.Quantity > cartItem.Product.Quantity)
    {
        TempData["Error"] = "Not enough quantity in stock.";
        return RedirectToAction("Index", "CartItems");
    }

    var order = new Order
    {
        UserId = cartItem.UserId,
        CreatedAt = DateTime.Now,
        Status = "Pending",
        TotalPrice = cartItem.Product.Price * cartItem.Quantity
    };

    _context.Orders.Add(order);
    await _context.SaveChangesAsync();

    var orderItem = new OrderItem
    {
        OrderId = order.Id,
        ProductId = cartItem.ProductId,
        Quantity = cartItem.Quantity,
        Price = cartItem.Product.Price
    };

    _context.OrderItems.Add(orderItem);
    await _context.SaveChangesAsync();

    return RedirectToAction("DeliveryForm", "Orders", new { id = order.Id });
}


    }
}