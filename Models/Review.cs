using System;
using System.Collections.Generic;

namespace JopShop.Models;

public partial class Review
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? ReviewText { get; set; }

    public byte? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
