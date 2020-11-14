using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSphere.Models.ViewModels.VM
{
    public class CreateVMViewModel
    {
        public string ApiId { get; set; }

        public string Name { get; set; }

        public int Memory { get; set; }

        public string OS { get; set; }

        public string DataStore { get; set; }

        public string Folder { get; set; }

        public string ResourcePool { get; set; }
    }
}
