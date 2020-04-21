using System;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class DataStoreViewModel : BaseViewModel
    {
        public string Datastore { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string FreeSpace { get; set; }

        public string Origem { get; set; }

        public string Capacity { get; set; }

        public DateTime Insert { get; set; }
    }
}
