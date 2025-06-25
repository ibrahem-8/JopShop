
using System.Diagnostics;
using JopShop.Models;
using JopShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace JopShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JopShopContext _context;

        public HomeController(ILogger<HomeController> logger, JopShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        // public IActionResult Index()
        // {
        //     var posts = _context.Posts
        //         .OrderByDescending(p => p.CreatedAt)
        //         .Take(10)
        //         .ToList();

        //     return View(posts);



        // }

public IActionResult Index()
{
    var posts = _context.Posts
        .OrderByDescending(p => p.CreatedAt)
        .Take(10)
        .ToList();

    var publishedUsers = _context.Users
        .Where(u => u.IsPublished && !(u.Suspended ?? false))
        .Select(u => new PublishedUserViewModel
        {
            Id = u.Id,
            UserName = u.UserName,
            ImagePath = u.ImagePath,
            AverageRating = _context.Ratings
                .Where(r => r.RatedUserId == u.Id)
                .Select(r => (double?)r.RatingValue)
                .Average() ?? 0
        })
        .ToList();

    var vm = new HomeViewModel
    {
        PublishedUsers = publishedUsers,
        LatestPosts = posts
    };

    return View(vm);
}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
