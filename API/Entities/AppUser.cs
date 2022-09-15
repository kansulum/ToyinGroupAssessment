using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {

        public ICollection<Todo> Todos { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}