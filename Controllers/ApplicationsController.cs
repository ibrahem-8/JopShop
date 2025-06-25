using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JopShop.Models;

namespace JopShop.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly JopShopContext _context;

        public ApplicationsController(JopShopContext context)
        {
            _context = context;
        }

   
        public async Task<IActionResult> Index()
{
    var currentUserId = HttpContext.Session.GetInt32("UserId");

    // إذا لم يكن المستخدم مسجل دخول → أعد توجيهه
    if (currentUserId == null)
        return RedirectToAction("Login", "LoginAndRegistr");

    // ✅ جلب الطلبات التي تخص صاحب الشغل فقط
    var myJobIds = await _context.Jobs
        .Where(j => j.EmployerId == currentUserId)
        .Select(j => j.Id)
        .ToListAsync();

    var applications = await _context.Applications
        .Include(a => a.Job)
        .Include(a => a.User)
        .Where(a => a.JobId.HasValue && myJobIds.Contains(a.JobId.Value))
        .ToListAsync();

    return View(applications);
}


        // ✅ GET: Applications/Create (تقديم على مشروع معين)
        public IActionResult Create(int jobId)
        {
            var userEmail = HttpContext.Session.GetString("CurrentUser"); // ✅ استخدم الجلسة
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
                return RedirectToAction("Login", "LoginAndRegistr");

            var job = _context.Jobs.FirstOrDefault(j => j.Id == jobId);
            if (job == null)
                return NotFound();

            ViewBag.JobTitle = job.Title;
            ViewBag.JobTitle = job.Title;
            ViewBag.Category = job.Category;
            ViewBag.ProjectDescription = job.Description;

            var application = new Application
            {
                JobId = jobId,
                UserId = user.Id, // ✅ استخدم ID من المستخدم الحقيقي
                Status = "Pending",
                AppliedAt = DateTime.Now
            };

            return View(application);
        }


        // // ✅ POST: Applications/Create
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(Application application)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(application);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction("Index", "Jobs");
        //     }

        //     return View(application);
        // }
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Application application)
{
    var userEmail = HttpContext.Session.GetString("CurrentUser");
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
    if (user == null)
        return RedirectToAction("Login", "LoginAndRegistr");

    // ✅ التحقق إذا قدم على نفس الوظيفة مسبقًا
    bool alreadyApplied = await _context.Applications
        .AnyAsync(a => a.UserId == user.Id && a.JobId == application.JobId);

    if (alreadyApplied)
    {
        TempData["Error"] = "I have already applied for this job.";
return RedirectToAction("Index", "Jobs");

    }

    // ✅ تقديم الطلب
    application.UserId = user.Id;
    application.AppliedAt = DateTime.Now;
    application.Status = "Pending";

    _context.Applications.Add(application);
    await _context.SaveChangesAsync();
    return RedirectToAction("Index", "Jobs");
}


        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
                return NotFound();

            return View(application);
        }

        // ✳️ (اختياري لاحقًا) GET + POST Edit/Delete

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
                return NotFound();

            return View(application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Application application)
        {
            if (id != application.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(application);
        }

        // حذف طلب
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
                return NotFound();

            return View(application);
        }

       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        //هاد الكود الجديد 
        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
        // public async Task<IActionResult> MyApplicants()
        // {
        //     var currentEmail = HttpContext.Session.GetString("CurrentUser");

        //     if (string.IsNullOrEmpty(currentEmail))
        //         return RedirectToAction("Login", "LoginAndRegistr");

        //     // جلب المشاريع الخاصة بالمستخدم الحالي
        //     var myJobIds = await _context.Jobs
        //         .Where(j => j.Employer.Email == currentEmail)
        //         .Select(j => j.Id)
        //         .ToListAsync();

        //     // جلب الطلبات المرتبطة بها
        //     var applications = await _context.Applications
        //         .Include(a => a.User)
        //         .Include(a => a.Job)
        //         .Where(a => myJobIds.Contains((int)a.JobId))
        //         .ToListAsync();

        //     return View(applications);
        // }
public async Task<IActionResult> MyApplicants()
{
    var currentUserId = HttpContext.Session.GetInt32("UserId");

    if (currentUserId == null)
        return RedirectToAction("Login", "LoginAndRegistr");

    // جلب الوظائف التي يملكها المستخدم الحالي
    var myJobIds = await _context.Jobs
        .Where(j => j.EmployerId == currentUserId) // ✅ ربط صحيح مع جدول Jobs
        .Select(j => j.Id)
        .ToListAsync();

    // جلب الطلبات المرتبطة بهذه الوظائف فقط
    var applications = await _context.Applications
        .Include(a => a.User)
        .Include(a => a.Job)
      
    .Where(a => a.JobId.HasValue && myJobIds.Contains(a.JobId.Value))
        .ToListAsync();

    return View(applications);
}

        // [HttpPost]
        // public async Task<IActionResult> Accept(int id)
        // {
        //     var app = await _context.Applications.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        //     if (app != null)
        //     {
        //         app.Status = "Accepted";
        //         await _context.SaveChangesAsync();

        //         // إرسال إيميل (مؤقتًا مجرد فتح رابط mailto)
        //         var mailto = $"mailto:{app.User?.Email}?subject=Accepted&body=You have been accepted.";
        //         return Redirect(mailto);
        //     }

        //     return RedirectToAction("Application");
        // }
[HttpPost]
public async Task<IActionResult> Accept(int id)
{
    var app = await _context.Applications.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
    if (app != null)
    {
        app.Status = "Accepted";
        await _context.SaveChangesAsync();

        // خزّن رابط البريد في TempData لفتحه بعد إعادة تحميل الصفحة
        TempData["mailto"] = $"mailto:{app.User?.Email}?subject=Accepted&body=You have been accepted.";
    }

    return RedirectToAction("Index");
}

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var app = await _context.Applications.FindAsync(id);
            if (app != null)
            {
                _context.Applications.Remove(app);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> FinishWork(int id)
        {
            var app = await _context.Applications.FindAsync(id);
            if (app != null)
            {
                app.Status = "Finished";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Rate(int id)
        {
            ViewBag.AppId = id;
            return View();
        }



[HttpPost]
public async Task<IActionResult> SubmitRating(int id, int rating)
{
    var app = await _context.Applications.FindAsync(id);
    if (app != null)
    {
        app.Status = "Rated";

        // 🔁 جلب المستخدم الحالي من الجلسة
        var email = HttpContext.Session.GetString("CurrentUser");
        var freelancer = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (freelancer == null)
            return RedirectToAction("Login", "LoginAndRegistr");

        // 🔁 إنشاء تقييم
        var newRating = new Rating
        {
            RatedUserId = app.UserId.Value,
            FreelancerId = freelancer.Id,        // المستخدم الذي قام بالتقييم
            RatingValue = rating,
            CreatedAt = DateTime.Now
        };

        _context.Ratings.Add(newRating);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction("Index");
}


        // [HttpPost]
        // public async Task<IActionResult> SubmitRating(int id, int rating)
        // {
        //     var app = await _context.Applications.FindAsync(id);
        //     if (app != null)
        //     {
        //         app.Status = "Rated";
        //         // يمكنك حفظ التقييم في جدول خاص
        //         await _context.SaveChangesAsync();
        //     }

        //     return RedirectToAction("");
        // }


    }


}













