// Models/Rating.cs
//created at 5
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JopShop.Models
{
    public class Rating
{
    public int Id { get; set; }
    public int RatedUserId { get; set; }   // الشخص الذي تم تقييمه
    public int FreelancerId { get; set; }  // الشخص الذي قام بالتقييم
    public int RatingValue { get; set; }
    public DateTime CreatedAt { get; set; }
//    public int UserId { get; set; }     
    public User RatedUser { get; set; }
    public User Freelancer { get; set; }
}

}
