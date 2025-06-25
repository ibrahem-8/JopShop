using System.Collections.Generic;
using JopShop.Models;

namespace JopShop.ViewModels
{
    public class HomeViewModel
    {
        public List<PublishedUserViewModel> PublishedUsers { get; set; }
        public List<Post> LatestPosts { get; set; } // لو كنت تعرض بوستات كمان
    }

    public class PublishedUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
    }
}
