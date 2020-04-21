using System;
using System.ComponentModel.DataAnnotations;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class UserViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(15, ErrorMessage = "Max {0}")]
        [MinLength(5, ErrorMessage = "Min {0}")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email")]
        [MaxLength(40, ErrorMessage = "Max {0}")]
        [EmailAddress(ErrorMessage = "email address is incorrect format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email")]
        [MaxLength(15, ErrorMessage = "Max {0}")]
        [MinLength(3, ErrorMessage = "Min {0}")]
        public string Password { get; set; }

        [Display(Name = "Insert Date")]
        public DateTime Insert { get; set; }
    }
}
