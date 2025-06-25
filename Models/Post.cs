using System;
using System.Collections.Generic;

namespace JopShop.Models;

public partial class Post
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? PostText { get; set; }
public string? Title { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
