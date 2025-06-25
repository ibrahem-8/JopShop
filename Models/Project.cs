using System;
using System.Collections.Generic;
//created at 5
namespace JopShop.Models;

public partial class Project
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Path { get; set; }
public string? Title { get; set; }

    public string? Description { get; set; }

    public virtual User? User { get; set; }
}
