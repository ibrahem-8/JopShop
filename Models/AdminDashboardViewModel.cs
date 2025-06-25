       using JopShop.Models;
       using System.Collections.Generic;

       namespace JopShop.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalProjects { get; set; }
        public int TotalUsers { get; set; }
        public int TotalOrders { get; set; }
        public int TotalPosts { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<User> AllUsers { get; set; }

        public List<Product> LatestProducts { get; set; }
        public List<Job> LatestJobs { get; set; }

        public List<User> LatestUsers { get; set; }
        public List<Post> LatestPosts { get; set; }

        public Dictionary<string, int> OrderStatusDistribution { get; set; }
        
        public int TotalSales { get; set; }
public Dictionary<string, int> MonthlySales { get; set; }

    }
}
