using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_ASP_NET.Models
{
    public class Role : IdentityRole<int>
    {
        public Role() : base() { }

        public Role(string role) : base(role) { }
    }
}
