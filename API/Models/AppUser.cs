using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public abstract class AppUser : IdentityUser<Guid>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}