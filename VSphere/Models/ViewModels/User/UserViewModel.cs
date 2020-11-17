using System;
using System.ComponentModel.DataAnnotations;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class UserViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Nome Completo")]
        [MaxLength(40, ErrorMessage = "Max permitido: {0}")]
        [MinLength(5, ErrorMessage = "Min permitido: {0}")]
        [Display(Name = "Nome Completo")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Endereço de E-mail está no formato incorreto")]
        [MaxLength(40, ErrorMessage = "Max permitido: {0}")]
        [MinLength(5, ErrorMessage = "Min permitido: {0}")]
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [MaxLength(15, ErrorMessage = "Max permitido: {0}")]
        [MinLength(3, ErrorMessage = "Min permitido: {0}")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Data de Inserção")]
        public DateTime Insert { get; set; }

        [Display(Name = "LockoutEnabled")]
        public bool LockoutEnabled { get; set; }
    }
}
