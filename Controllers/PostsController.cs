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
//     public class PostsController : Controller
//     {
//         private readonly JopShopContext _context;

//         public PostsController(JopShopContext context)
//         {
//             _context = context;
//         }

//         // GET: Posts
//         public async Task<IActionResult> Index()
//         {
//             var jopShopContext = _context.Posts.Include(p => p.User);
//             return View(await jopShopContext.ToListAsync());
//         }

//         // GET: Posts/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var post = await _context.Posts
//                 .Include(p => p.User)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (post == null)
//             {
//                 return NotFound();
//             }

//             return View(post);
//         }

//         // GET: Posts/Create
//         public IActionResult Create()
//         {
//             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
//             return View();
//         }

        
//         [HttpPost]
//         [ValidateAntiForgeryToken]
        

//         // GET: Posts/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var post = await _context.Posts.FindAsync(id);
//             if (post == null)
//             {
//                 return NotFound();
//             }
//             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
//             return View(post);
//         }

//         // POST: Posts/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PostText,CreatedAt")] Post post)
//         {
//             if (id != post.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(post);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!PostExists(post.Id))
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
//             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
//             return View(post);
//         }

//         // GET: Posts/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var post = await _context.Posts
//                 .Include(p => p.User)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (post == null)
//             {
//                 return NotFound();
//             }

//             return View(post);
//         }

//         // POST: Posts/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var post = await _context.Posts.FindAsync(id);
//             if (post != null)
//             {
//                 _context.Posts.Remove(post);
//             }

//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool PostExists(int id)
//         {
//             return _context.Posts.Any(e => e.Id == id);
//         }
//        // POST: Posts/Create
// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> Create([Bind("Title,PostText,CreatedAt")] Post post)
// {
//     post.UserId = 28;

//     if (ModelState.IsValid)
//     {
//         _context.Add(post);
//         await _context.SaveChangesAsync();
//         return RedirectToAction("Index", "Admin");
//     }

//     return View(post);
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
    public class PostsController : Controller
    {
        private readonly JopShopContext _context;

        public PostsController(JopShopContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var jopShopContext = _context.Posts.Include(p => p.User);
            return View(await jopShopContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null) return NotFound();

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,PostText,CreatedAt")] Post post)
        {
            post.UserId = 28; // ربط البوست بالأدمن

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Admin");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PostText,CreatedAt,Title")] Post post)
        {
            if (id != post.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);
            if (post == null) return NotFound();

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null) _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
          return RedirectToAction("Index", "Home");

        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
