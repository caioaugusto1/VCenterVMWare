using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSphere.Models.Identity
{
    public class ApplicationIdentityUser : IdentityUser
    {

        public string FullName { get; set; }
        public DateTime Insert { get; set; }
    }
}
