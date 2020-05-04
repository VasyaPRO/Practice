using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_ASP_NET.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(44)]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only")]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Weight { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        [StringLength(74)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(777)]
        public string Description { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
    }
}
