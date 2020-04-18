using System.ComponentModel.DataAnnotations;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class ServerViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Server IP is required")]
        public string IP { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
