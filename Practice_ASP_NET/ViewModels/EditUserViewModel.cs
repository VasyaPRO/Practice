using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_ASP_NET.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
