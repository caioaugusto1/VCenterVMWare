using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using VSphere.Models.Identity;

namespace VSphere.Models.Identity
{
    public class EditRoleViewModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationIdentityUser> Members { get; set; }
        public IEnumerable<ApplicationIdentityUser> NonMembers { get; set; }
    }
}
