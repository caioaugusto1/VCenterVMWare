using System;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class HostViewModel : BaseViewModel
    {
        public string Host { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string Power { get; set; }

        public string Origem { get; set; }

        public DateTime Insert { get; private set; }
    }
}
