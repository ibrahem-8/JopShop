//created at 5

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace JopShop.Models
{
    public partial class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Display(Name = "Profile Image")]
        public string? ImagePath { get; set; }

        [Display(Name = "LinkedIn URL")]
        public string? LinkedInUrl { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string? Role { get; set; }


        public string? CvPath { get; set; }

        public string? Description { get; set; }

        public bool? Suspended { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // لرفع الصورة
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Gender")]
        public string? Gender { get; set; }



        [NotMapped]
        public IFormFile? CvFile { get; set; }



        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        public virtual ICollection<Rating> GivenRatings { get; set; } = new List<Rating>();


        public bool IsPublished { get; set; } = false;





        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();


        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<ProjectsUser> ProjectsUser { get; set; } = new List<ProjectsUser>();


    }
}