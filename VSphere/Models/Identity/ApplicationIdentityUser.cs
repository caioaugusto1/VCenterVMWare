using Microsoft.AspNetCore.Identity;
using System;

namespace VSphere.Models.Identity
{
    public class ApplicationIdentityUser : IdentityUser
    {

        public string FullName { get; set; }
        public DateTime Insert { get; set; }
    }
}
