// using JopShop.Models;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace JopShop.Controllers
// {
//     public class ProjectsUserController : Controller
//     {
//         private readonly JopShopContext _context;
//         private readonly IWebHostEnvironment _webHostEnvironment;

//         public ProjectsUserController(JopShopContext context, IWebHostEnvironment webHostEnvironment)
//         {
//             _context = context;
//             _webHostEnvironment = webHostEnvironment;
//         }

//         // ✅ إنشاء مشروع جديد
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(string Description, IFormFile FileUpload)
//         {
//             if (string.IsNullOrEmpty(Description) || FileUpload == null)
//             {
//                 TempData["Error"] = "Please provide both a description and a file.";
//                 return RedirectToAction("Profile", "Users");
//             }

//             // حفظ الملف
//             string fileName = Guid.NewGuid() + Path.GetExtension(FileUpload.FileName);
//             string path = Path.Combine(_webHostEnvironment.WebRootPath, "projects", fileName);

//             using (var stream = new FileStream(path, FileMode.Create))
//             {
//                 await FileUpload.CopyToAsync(stream);
//             }

//             // الحصول على المستخدم الحالي
//             var email = HttpContext.Session.GetString("CurrentUser");
//             var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
//             if (user == null)
//                 return RedirectToAction("Login", "LoginAndRegistr");

//             // حفظ المشروع في قاعدة البيانات
//             var project = new ProjectsUser
//             {
//                 Title = Path.GetFileNameWithoutExtension(fileName),
//                 Description = Description,
//                 FileName = fileName,
//                 Path = "/projects/" + fileName,
//                 CreatedAt = DateTime.Now,
//                 UserId = user.Id
//             };

//             _context.ProjectsUser.Add(project);
//             await _context.SaveChangesAsync();

//             return RedirectToAction("Profile", "Users");
//         }

//         // ✅ صفحة التعديل
//         public async Task<IActionResult> Edit(int id)
//         {
//             var project = await _context.ProjectsUser.FindAsync(id);
//             if (project == null) return NotFound();
//             return View(project);
//         }

//         // ✅ حفظ التعديلات
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, string Description, IFormFile FileUpload)
//         {
//             var project = await _context.ProjectsUser.FindAsync(id);
//             if (project == null) return NotFound();

//             project.Description = Description;

//             if (FileUpload != null)
//             {
//                 string fileName = Guid.NewGuid() + Path.GetExtension(FileUpload.FileName);
//                 string path = Path.Combine(_webHostEnvironment.WebRootPath, "projects", fileName);

//                 using (var stream = new FileStream(path, FileMode.Create))
//                 {
//                     await FileUpload.CopyToAsync(stream);
//                 }

//                 project.Path = "/projects/" + fileName;
//                 project.Title = Path.GetFileNameWithoutExtension(fileName);
//                 project.FileName = fileName;
//             }

//             _context.ProjectsUser.Update(project);
//             await _context.SaveChangesAsync();

//             return RedirectToAction("Profile", "Users");
//         }

//         // ✅ حذف المشروع
//         public async Task<IActionResult> Delete(int id)
//         {
//             var project = await _context.ProjectsUser.FindAsync(id);
//             if (project == null) return NotFound();

//             _context.ProjectsUser.Remove(project);
//             await _context.SaveChangesAsync();

//             return RedirectToAction("Profile", "Users");
//         }
//     }
// }
using JopShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JopShop.Controllers
{
    public class ProjectsUserController : Controller
    {
        private readonly JopShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectsUserController(JopShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // ✅ إنشاء مشروع جديد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Description, IFormFile FileUpload)
        {
            if (string.IsNullOrEmpty(Description) || FileUpload == null)
            {
                TempData["Error"] = "Please provide both a description and a file.";
                return RedirectToAction("Profile", "Users");
            }

            // حفظ الملف
            string fileName = Guid.NewGuid() + Path.GetExtension(FileUpload.FileName);
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "projects", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await FileUpload.CopyToAsync(stream);
            }

            // الحصول على المستخدم الحالي
            var email = HttpContext.Session.GetString("CurrentUser");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return RedirectToAction("Login", "LoginAndRegistr");

            // حفظ المشروع في قاعدة البيانات
            var project = new ProjectsUser
            {
                Title = Path.GetFileNameWithoutExtension(fileName),
                Description = Description,
                FileName = fileName,
                Path = "/projects/" + fileName,
                CreatedAt = DateTime.Now,
                UserId = user.Id
            };

            _context.ProjectsUser.Add(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", "Users");
        }

        // ✅ صفحة التعديل
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.ProjectsUser.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // ✅ حفظ التعديلات
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Description, IFormFile FileUpload)
        {
            var project = await _context.ProjectsUser.FindAsync(id);
            if (project == null) return NotFound();

            project.Description = Description;

            if (FileUpload != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(FileUpload.FileName);
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "projects", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(stream);
                }

                project.Path = "/projects/" + fileName;
                project.Title = Path.GetFileNameWithoutExtension(fileName);
                project.FileName = fileName;
            }

            _context.ProjectsUser.Update(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", "Users");
        }

        // ✅ حذف المشروع
        public async Task<IActionResult> Delete(int id)
       {
    var project = await _context.ProjectsUser
        .AsNoTracking()  // ⬅️ نمنع التتبع لتفادي مشاكل الـConcurrency
        .FirstOrDefaultAsync(p => p.Id == id);

    if (project == null)
        return NotFound();

    _context.Entry(project).State = EntityState.Deleted;
    await _context.SaveChangesAsync();

    return RedirectToAction("Profile", "Users");
}



        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> SaveProject(int? Id, string Description, IFormFile FileUpload)
{
    var email = HttpContext.Session.GetString("CurrentUser");
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    if (user == null) return RedirectToAction("Login", "LoginAndRegistr");

    ProjectsUser project;

    if (Id.HasValue)
    {
        // تعديل
        project = await _context.ProjectsUser.FindAsync(Id.Value);
        if (project == null || project.UserId != user.Id)
            return NotFound();

        project.Description = Description;

        if (FileUpload != null)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(FileUpload.FileName);
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "projects", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await FileUpload.CopyToAsync(stream);
            }

            project.Path = "/projects/" + fileName;
            project.Title = Path.GetFileNameWithoutExtension(fileName);
            project.FileName = fileName;
        }

        _context.ProjectsUser.Update(project);
    }
    else
    {
        // إضافة
        if (FileUpload == null || string.IsNullOrEmpty(Description))
        {
            TempData["Error"] = "Please provide both file and description.";
            return RedirectToAction("Profile", "Users", new { email = user.Email });
        }

        string fileName = Guid.NewGuid() + Path.GetExtension(FileUpload.FileName);
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "projects", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await FileUpload.CopyToAsync(stream);
        }

        project = new ProjectsUser
        {
            UserId = user.Id,
            Description = Description,
            Title = Path.GetFileNameWithoutExtension(fileName),
            Path = "/projects/" + fileName,
            FileName = fileName,
            CreatedAt = DateTime.Now
        };

        _context.ProjectsUser.Add(project);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction("Profile", "Users", new { email = user.Email });
}

    }
}
