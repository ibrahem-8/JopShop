using System;
using System.Collections.Generic;
//created at 5
namespace JopShop.Models;

public partial class Application
{
    public int Id { get; set; }

    public int? JobId { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public DateTime? AppliedAt { get; set; }

    public virtual Job? Job { get; set; }

    public virtual User? User { get; set; }
    
}
