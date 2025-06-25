using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JopShop.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
