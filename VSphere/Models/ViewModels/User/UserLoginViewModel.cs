using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VSphere.Models
{
    public class UserLoginViewModel
    {
        [EmailAddress(ErrorMessage = "Email address is incorrect format")]
        [MaxLength(40, ErrorMessage = "Max length is: {0}")]
        [MinLength(5, ErrorMessage = "Min length is: {0}")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
