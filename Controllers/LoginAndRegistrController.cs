// using JopShop.Models;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace JopShop.Controllers
// {
//     public class LoginAndRegistrController : Controller
//     {
//         private readonly JopShopContext _context;
//         private readonly IWebHostEnvironment _webHostEnvironment;

//         public LoginAndRegistrController(JopShopContext context, IWebHostEnvironment webHostEnvironment)
//         {
//             _context = context;
//             _webHostEnvironment = webHostEnvironment;
//         }

//         public IActionResult Register() => View();
//         public IActionResult Register2() => View();
//         public IActionResult Login() => View();


// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> Register2(User user)
// {
//     if (ModelState.IsValid)
//     {
//         // ✅ رفع صورة البروفايل
//         if (user.ImageFile != null)
//         {
//             string imageFileName = Guid.NewGuid() + Path.GetExtension(user.ImageFile.FileName);
//             string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageFileName);

//             using (var stream = new FileStream(imagePath, FileMode.Create))
//             {
//                 await user.ImageFile.CopyToAsync(stream);
//             }

//             user.ImagePath = "/images/" + imageFileName;
//         }

//         // ✅ رفع السيرة الذاتية
//         if (user.CvFile != null)
//         {
//             string cvFileName = Guid.NewGuid() + Path.GetExtension(user.CvFile.FileName);
//             string cvPath = Path.Combine(_webHostEnvironment.WebRootPath, "cv", cvFileName);

//             using (var stream = new FileStream(cvPath, FileMode.Create))
//             {
//                 await user.CvFile.CopyToAsync(stream);
//             }

//             user.CvPath = "/cv/" + cvFileName;
//         }

//         // ✅ بيانات افتراضية
//         user.Role = "user";
//         user.Suspended = false;
//         user.CreatedAt = DateTime.Now;
//         user.UpdatedAt = DateTime.Now;

//         _context.Add(user);
//         await _context.SaveChangesAsync();

//         return RedirectToAction("Login", "LoginAndRegistr");
//     }

//     return View(user);
// }


// [HttpPost]
// [ValidateAntiForgeryToken]
// public IActionResult Login(User userLogin)
// {
//     var auth = _context.Users.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);

//     if (auth == null)
//     {
//         ModelState.AddModelError("", "Invalid email or password.");
//         return View();
//     }

//     // ✅ تحقق من حالة التعليق
//     if (auth.Suspended == true)
//     {
//         ModelState.AddModelError("", "Your account is suspended. Please contact support.");
//         return View();
//     }

//     // ✅ التوجيه حسب الدور
//     switch (auth.Role)
//     {
//         case "admin":
//             HttpContext.Session.SetString("CurrentUser", auth.Email);
//             HttpContext.Session.SetString("AdminName", auth.Email);
//             return RedirectToAction("Index", "Admin");

//         case "user":
//             HttpContext.Session.SetInt32("UserId", auth.Id);
//             HttpContext.Session.SetString("CurrentUser", auth.Email);
//             return RedirectToAction("Index", "Home");

//         default:
//             ModelState.AddModelError("", "Invalid role.");
//             return View();
//     }
// }


//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult RegisterStep1(string Email, string Password, string ConfirmPassword)
//         {
//             if (Password != ConfirmPassword)
//             {
//                 TempData["Error"] = "Passwords do not match.";
//                 return RedirectToAction("Register");
//             }

//             TempData["Email"] = Email;
//             TempData["Password"] = Password;
//             TempData.Keep(); // احتفظ بالبيانات حتى الصفحة التالية

//             return RedirectToAction("Register2");
//         }
//     }
// }

using JopShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace JopShop.Controllers
{
    public class LoginAndRegistrController : Controller
    {
        private readonly JopShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginAndRegistrController(JopShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Register() => View(new RegistrationModel() { });
        public IActionResult Register2() => View(new User() { });
        public IActionResult Login() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register2(User user)
        {

            user.Email = TempData["Email"].ToString();
            user.Password = TempData["Password"].ToString();

            ModelState.Clear();

            TryValidateModel(user);

            if (_context.Users.Any(p => p.UserName == user.UserName))
            {
                ViewBag.Errors = "Username is already taken.";
                return View(user);
            }

            if (ModelState.IsValid)
            {
                // ✅ رفع صورة البروفايل
                if (user.ImageFile != null)
                {
                    string imageFileName = Guid.NewGuid() + Path.GetExtension(user.ImageFile.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(stream);
                    }

                    user.ImagePath = "/images/" + imageFileName;
                }

                // ✅ رفع السيرة الذاتية
                if (user.CvFile != null)
                {
                    string cvFileName = Guid.NewGuid() + Path.GetExtension(user.CvFile.FileName);
                    string cvPath = Path.Combine(_webHostEnvironment.WebRootPath, "cv", cvFileName);

                    using (var stream = new FileStream(cvPath, FileMode.Create))
                    {
                        await user.CvFile.CopyToAsync(stream);
                    }

                    user.CvPath = "/cv/" + cvFileName;
                }

                // ✅ بيانات افتراضية
                user.Role = "user";
                user.Suspended = false;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                user.Password = CreateMD5(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "LoginAndRegistr");
            }

            return View(user);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User userLogin)
        {

            userLogin.Password = CreateMD5(userLogin.Password);

            var auth = _context.Users.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);

            if (auth == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View();
            }

            // ✅ تحقق من حالة التعليق
            if (auth.Suspended == true)
            {
                ModelState.AddModelError("", "Your account is suspended. Please contact support.");
                return View();
            }

            switch (auth.Role)
            {
                case "admin":
                    HttpContext.Session.SetString("CurrentUser", auth.Email);
                    HttpContext.Session.SetString("AdminName", auth.Email);
                    return RedirectToAction("Index", "Admin");

                case "user":
                    HttpContext.Session.SetInt32("UserId", auth.Id);
                    HttpContext.Session.SetString("CurrentUser", auth.Email); // هذا السطر المهم
                    return RedirectToAction("Index", "Home");


                default:
                    ModelState.AddModelError("", "Invalid role.");
                    return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterStep1(string email, string password, string confirmPassword)
        {

            var registrationModel = new RegistrationModel()
            {
                Email = email
            };

            if (_context.Users.Any(u => u.Email == email))
            {
                registrationModel.ErrorMessages = "Email is already registered.";
                return View("Register", registrationModel);

            }

            if (password != confirmPassword)
            {
                registrationModel.ErrorMessages = "Passwords do not match.";
                return View("Register", registrationModel);

            }

            if (password.Length < 8 ||
                !password.Any(char.IsUpper) ||
                !password.Any(char.IsLower) ||
                !password.Any(char.IsDigit) ||
                !password.Any(ch => "!@#$%^&*()_+-=[]{}|;:',.<>/?".Contains(ch)))
            {
                registrationModel.ErrorMessages = "Password must be at least 8 characters and include upper, lower, number, and special character.";
                return View("Register", registrationModel);

            }

            TempData["Email"] = email;
            TempData["Password"] = password;
            TempData.Keep(); // احتفظ بالبيانات حتى الصفحة التالية

            return RedirectToAction("Register2");
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
    }
}
















//using JopShop.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace JopShop.Controllers
//{
//    public class LoginAndRegistrController : Controller
//    {
//        private readonly JopShopContext _context;
//        private readonly IWebHostEnvironment _webHostEnviroment;

//        public LoginAndRegistrController(JopShopContext context, IWebHostEnvironment webHostEnviroment)
//        {
//            _context = context;
//            _webHostEnviroment = webHostEnviroment;
//        }

//        //the method is get by default
//        public IActionResult Register()
//        {
//            return View();
//        }

//        public IActionResult Register2()
//        {
//            return View();
//        }

//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register2([Bind("Id,Name,Email,Password,Role,CvPath,Description,Suspended,CreatedAt,UpdatedAt")] User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(user);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));

//                //save user sign up to database
//                _context.Add(user);
//                await _context.SaveChangesAsync();

//                return RedirectToAction("Login", "LoginAndRegistr");
//            }

//            return View(user);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Login(User userLogin)
//        {
//            //Authntication: find the user based on email and pass
//            var auth = _context.Users
//                .Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password)
//                .SingleOrDefault();

//            //authorization: check the user roleId
//            switch (auth.Role)
//            {
//                //if the user is admin (Role == "admin"), store the email in session and redirect to the admin index page
//                case "admin":
//                    HttpContext.Session.SetString("AdminName", auth.Email);
//                    return RedirectToAction("Index", "Admin");

//                //if the user is user (Role == "user"), store the userId in session and redirect to the home index page
//                case "user":
//                    HttpContext.Session.SetInt32("UserId", (int)auth.Id);
//                    return RedirectToAction("Index", "Home");

//                //if the user has invalid role, add an error and return to the login view
//                default:
//                    ModelState.AddModelError("", "Invalid role.");
//                    return View();

//            }
//        }


//    }
//}






