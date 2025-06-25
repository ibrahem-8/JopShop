


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JopShop.Models;
using Microsoft.AspNetCore.Http;

namespace JopShop.Controllers
{
    public class JobsController : Controller
    {
        private readonly JopShopContext _context;

        public JobsController(JopShopContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var jopShopContext = _context.Jobs.Include(j => j.Employer);
            ViewBag.CurrentUser = HttpContext.Session.GetString("CurrentUser"); // ✅
            return View(await jopShopContext.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var job = await _context.Jobs.Include(j => j.Employer).FirstOrDefaultAsync(j => j.Id == id);
            if (job == null) return NotFound();

            ViewBag.CurrentUser = HttpContext.Session.GetString("CurrentUser"); // ✅
            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            //     ViewData["EmployerId"] = new SelectList(_context.Users, "Id", "Id");
            //     return View();
            //
            var currentEmail = HttpContext.Session.GetString("CurrentUser");
    if (string.IsNullOrEmpty(currentEmail))
        return RedirectToAction("Login", "LoginAndRegistr"); // ⬅️ إعادة توجيه لصفحة الدخول

    return View(); }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Job job, IFormFile? AttachmentFile)
        {
            if (ModelState.IsValid)
            {
                // ✅ استرجاع البريد من الجلسة
                var currentEmail = HttpContext.Session.GetString("CurrentUser");
                var employer = _context.Users.FirstOrDefault(u => u.Email == currentEmail);
                if (employer != null)
                {
                    job.EmployerId = employer.Id; // ✅ ربط المشروع بصاحب الإيميل
                }

                // معالجة المرفق
                if (AttachmentFile != null && AttachmentFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    Directory.CreateDirectory(uploadsFolder);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(AttachmentFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await AttachmentFile.CopyToAsync(stream);
                    }
                }

                job.CreatedAt = DateTime.Now;
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployerId"] = new SelectList(_context.Users, "Id", "Id", job.EmployerId);
            return View(job);
        }


        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var job = await _context.Jobs.Include(j => j.Employer).FirstOrDefaultAsync(j => j.Id == id);
            if (job == null) return NotFound();

            var currentUser = HttpContext.Session.GetString("CurrentUser");
            if (job.Employer?.Email != currentUser)
                return Unauthorized();

            ViewData["EmployerId"] = new SelectList(_context.Users, "Id", "Id", job.EmployerId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Job job, IFormFile? AttachmentFile)
        {
            if (id != job.Id) return NotFound();

            var originalJob = await _context.Jobs.Include(j => j.Employer).FirstOrDefaultAsync(j => j.Id == id);
            var currentUser = HttpContext.Session.GetString("CurrentUser");
            if (originalJob?.Employer?.Email != currentUser)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    if (AttachmentFile != null && AttachmentFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        Directory.CreateDirectory(uploadsFolder);
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(AttachmentFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await AttachmentFile.CopyToAsync(stream);
                        }
                    }

                    // تحديث البيانات الأساسية
                    originalJob.Title = job.Title;
                    originalJob.Description = job.Description;
                    originalJob.Category = job.Category;
                    originalJob.Location = job.Location;
                    originalJob.Salary = job.Salary;

                    _context.Update(originalJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployerId"] = new SelectList(_context.Users, "Id", "Id", job.EmployerId);
            return View(job);
        }
// ✅ هذا هو الميثود الصحيح لحذف مشروع باستخدام Modal
 [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _context.Jobs.Include(j => j.Employer).FirstOrDefaultAsync(j => j.Id == id);
            var currentUser = HttpContext.Session.GetString("CurrentUser");

            if (job == null || job.Employer?.Email != currentUser)
                return Unauthorized();

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }





        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}



