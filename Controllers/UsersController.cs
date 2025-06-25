

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JopShop.Models;
using JopShop.Models.ViewModels;

namespace JopShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly JopShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(JopShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Role,CvPath,Description,PhoneNumber,Gender,Suspended,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(user);

            var userInDb = await _context.Users.FindAsync(id);
            if (userInDb == null)
                return NotFound();

           // ✅ تحديث البيانات (مع الحفاظ على القيم القديمة لو ما وصلت من الفورم)
        userInDb.Name = user.Name ?? userInDb.Name;
        userInDb.PhoneNumber = user.PhoneNumber ?? userInDb.PhoneNumber;    
        userInDb.Gender = user.Gender ?? userInDb.Gender;
        userInDb.LinkedInUrl = user.LinkedInUrl ?? userInDb.LinkedInUrl;
        userInDb.Description = user.Description ?? userInDb.Description;
        userInDb.UpdatedAt = DateTime.Now;
        userInDb.Email = user.Email ?? userInDb.Email;
        userInDb.Password = user.Password ?? userInDb.Password;
        userInDb.Role = string.IsNullOrEmpty(user.Role) ? userInDb.Role : user.Role;
        userInDb.CreatedAt = user.CreatedAt ?? userInDb.CreatedAt;
        userInDb.Suspended = user.Suspended ?? userInDb.Suspended;

            // ✅ صورة جديدة
            if (user.ImageFile != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(user.ImageFile.FileName);
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await user.ImageFile.CopyToAsync(stream);
                }

                userInDb.ImagePath = "/images/" + fileName;
            }

            // ✅ CV جديد
            if (user.CvFile != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(user.CvFile.FileName);
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "cv", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await user.CvFile.CopyToAsync(stream);
                }

                userInDb.CvPath = "/cv/" + fileName;
            }

            _context.Update(userInDb);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", new { email = userInDb.Email });
        }
        // ✅ عرض صفحة البروفايل
        // public async Task<IActionResult> Profile(string? email)
        // {
        //     if (string.IsNullOrEmpty(email))
        //         email = HttpContext.Session.GetString("CurrentUser");

        //     if (string.IsNullOrEmpty(email))
        //         return RedirectToAction("Login", "LoginAndRegistr");

        //     var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
        //     var isOwner = string.Equals(email.Trim(), currentUserEmail?.Trim(), StringComparison.OrdinalIgnoreCase);

        //     var user = await _context.Users
        //         .Include(u => u.ProjectsUser)  // ✅ ربط مشاريع المستخدم الجديدة
        //         .FirstOrDefaultAsync(u => u.Email == email);

        //     if (user == null)
        //         return NotFound();

        //     var vm = new ProfileViewModel
        //     {
        //         User = user,
        //         ProjectsUser = user.ProjectsUser.ToList(),  // ✅ استخدم ProjectsUser بدلاً من Projects
        //         IsOwner = isOwner
        //     };

        //     return View("Profile", vm);


        // }
        public async Task<IActionResult> Profile(string? email, int? id)
{
    User user;

    if (id.HasValue)
    {
        user = await _context.Users
            .Include(u => u.Ratings)
            .Include(u => u.ProjectsUser)
            .FirstOrDefaultAsync(u => u.Id == id.Value);
    }
    else
    {
        email ??= HttpContext.Session.GetString("CurrentUser");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "LoginAndRegistr");

        user = await _context.Users
            .Include(u => u.Ratings)
            .Include(u => u.ProjectsUser)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    if (user == null)
        return NotFound();

    var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
    var isOwner = string.Equals(user.Email?.Trim(), currentUserEmail?.Trim(), StringComparison.OrdinalIgnoreCase);

    var vm = new ProfileViewModel
    {
        User = user,
        ProjectsUser = user.ProjectsUser?.ToList() ?? new List<ProjectsUser>(),
        Ratings = user.Ratings?.ToList() ?? new List<Rating>(),
        AverageRating = user.Ratings?.Any() == true ? user.Ratings.Average(r => r.RatingValue) : 0,
        RatingCount = user.Ratings?.Count ?? 0,
        IsOwner = isOwner
    };

    return View("Profile", vm);
}

        
// public async Task<IActionResult> Profile(string? email)
        // {
        //     if (string.IsNullOrEmpty(email))
        //         email = HttpContext.Session.GetString("CurrentUser");

        //     if (string.IsNullOrEmpty(email))
        //         return RedirectToAction("Login", "LoginAndRegistr");

        //     var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
        //     var isOwner = string.Equals(email.Trim(), currentUserEmail?.Trim(), StringComparison.OrdinalIgnoreCase);

        //     var user = await _context.Users
        //         .Include(u => u.Ratings) // ✅ أضف هذه
        //         .Include(u => u.ProjectsUser)
        //         .FirstOrDefaultAsync(u => u.Email == email);

        //     if (user == null)
        //         return NotFound();

        //     var vm = new ProfileViewModel
        //     {
        //         User = user,
        //         ProjectsUser = user.ProjectsUser?.ToList() ?? new List<ProjectsUser>(),
        //         Ratings = user.Ratings.ToList(), // ✅ مهمة
        //         AverageRating = user.Ratings.Any() ? user.Ratings.Average(r => r.RatingValue) : 0, // ✅ مهمة
        //         RatingCount = user.Ratings.Count,
        //         IsOwner = isOwner
        //     };

        //     return View("Profile", vm);
        // }

        // [Route("Users/ProfileById/{id}")]
        public async Task<IActionResult> ProfileById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Ratings)
                .Include(u => u.ProjectsUser) // ✅ أضف هذه السطر
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new ProfileViewModel
            {
                User = user,
                ProjectsUser = user.ProjectsUser?.ToList() ?? new List<ProjectsUser>(), // ✅ حتى لا تكون null
                AverageRating = user.Ratings.Any() ? user.Ratings.Average(r => r.RatingValue) : 0,
                RatingCount = user.Ratings.Count,
                Ratings = user.Ratings.ToList(),
                IsOwner = false // أو حسب منطقك
            };

            return View("Profile", viewModel);
        }










        
    }
}



        // 
        // public async Task<IActionResult> ProfileById(int id)
        // {
        //     var user = await _context.Users
        //         .Include(u => u.ProjectsUser)
        //         .FirstOrDefaultAsync(u => u.Id == id);

        //     if (user == null)
        //         return NotFound();

        //     var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
        //     var isOwner = string.Equals(user.Email?.Trim(), currentUserEmail?.Trim(), StringComparison.OrdinalIgnoreCase);

        //     var vm = new ProfileViewModel
        //     {
        //         User = user,
        //         ProjectsUser = user.ProjectsUser?.ToList(),
        //         IsOwner = isOwner
        //     };

        //     return View("Profile", vm);
        // }




        // public async Task<IActionResult> ProfileById(int id)
        // {
        //     var user = await _context.Users
        //         .Include(u => u.Ratings)
        //         .FirstOrDefaultAsync(u => u.Id == id);

        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     var viewModel = new ProfileViewModel
        //     {
        //         User = user,
        //         AverageRating = user.Ratings.Any() ? user.Ratings.Average(r => r.RatingValue) : 0,
        //         RatingCount = user.Ratings.Count
        //     };

        //     return View("Profile", viewModel);
        // }




 // // ✅ عرض صفحة البروفايل
        // public async Task<IActionResult> Profile(string? email)
        // {
        //     if (string.IsNullOrEmpty(email))
        //         email = HttpContext.Session.GetString("CurrentUser");

        //     if (string.IsNullOrEmpty(email))
        //         return RedirectToAction("Login", "LoginAndRegistr");

        //     var currentUserEmail = HttpContext.Session.GetString("CurrentUser");
        //     var isOwner = string.Equals(email.Trim(), currentUserEmail?.Trim(), StringComparison.OrdinalIgnoreCase);

        //     var user = await _context.Users
        //         .Include(u => u.Projects)
        //         .FirstOrDefaultAsync(u => u.Email == email);

        //     if (user == null)
        //         return NotFound();

        //     var vm = new ProfileViewModel
        //     {
        //         User = user,
        //         Projects = user.Projects.ToList(),
        //         IsOwner = isOwner
        //     };

        //     return View("Profile", vm);
        // }








