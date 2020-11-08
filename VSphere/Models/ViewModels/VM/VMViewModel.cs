using Newtonsoft.Json;
using System;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class VMViewModel : BaseViewModel
    {
        public string Memory { get; set; }

        public string VM { get; set; }

        public string Name { get; set; }

        public string Power { get; set; }

        public int CPU { get; set; }

        public string Origem { get; set; }

        public DateTime Insert { get; private set; }

        public bool GetOnlinesVM { get; set; } = false;

    }
}
