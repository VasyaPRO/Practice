using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Practice_ASP_NET.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}