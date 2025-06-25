using System.Collections.Generic;
using JopShop.Models;

namespace JopShop.Models.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }

        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public bool IsOwner { get; set; }
        public List<ProjectsUser> ProjectsUser { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Rating> Ratings { get; set; } = new();

    }
    
    
 
}

