using System.ComponentModel.DataAnnotations;

namespace VSphere.Models.Identity
{
    public class ModifyRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToRemove { get; set; }
    }
}
