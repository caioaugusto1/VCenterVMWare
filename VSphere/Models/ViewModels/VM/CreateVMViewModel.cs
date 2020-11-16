using System.ComponentModel.DataAnnotations;

namespace VSphere.Models.ViewModels.VM
{
    public class CreateVMViewModel
    {
        public string ApiId { get; set; }

        [Display(Name = "Nome da Máquina")]
        public string Name { get; set; }

        [Display(Name = "Memória")]
        public int Memory { get; set; }

        [Display(Name = "Sistema Operacional")]
        public string OS { get; set; }

        [Display(Name = "Datastore")]
        public string DataStore { get; set; }

        [Display(Name = "Pasta")]
        public string Folder { get; set; }

        [Display(Name = "Resource Pool")]
        public string ResourcePool { get; set; }

        [Display(Name = "Rede")]
        public string Networking { get; set; }
    }
}
