
using System;
using System.Collections.Generic;

namespace JopShop.Models;
//created at 5
public partial class Job
{

    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? Location { get; set; }
    public decimal Salary { get; set; } // نستخدمه كـ Budget
                                        //التعديل الجديد
                                        //     public string? Duration { get; set; }
                                        //     public string? Currency { get; set; }
                                        //    public string? PaymentType { get; set; }
                                        //
    public int? EmployerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual User? Employer { get; set; }
    
}