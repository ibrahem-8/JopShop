using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JopShop.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    public string? ImageUrl { get; set; }

    [NotMapped]
   
public IFormFile? ImageFile { get; set; }

public bool IsHidden { get; set; }


    public int? SellerId { get; set; }

    public DateTime? CreatedAt { get; set; }
public int? Quantity { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? Seller { get; set; }
}