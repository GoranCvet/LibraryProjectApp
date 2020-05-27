using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.Domain.Authentication
{
    public class AppUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
