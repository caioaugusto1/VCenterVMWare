using System;
using System.ComponentModel.DataAnnotations;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class UserViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Fullname is required")]
        [MaxLength(40, ErrorMessage = "Max length is: {0}")]
        [MinLength(5, ErrorMessage = "Min length is: {0}")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Email address is incorrect format")]
        [MaxLength(40, ErrorMessage = "Max length is: {0}")]
        [MinLength(5, ErrorMessage = "Min length is: {0}")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(15, ErrorMessage = "Max length is: {0}")]
        [MinLength(3, ErrorMessage = "Min length is: {0}")]
        public string Password { get; set; }

        [Display(Name = "Insert Date")]
        public DateTime Insert { get; set; }
    }
}
