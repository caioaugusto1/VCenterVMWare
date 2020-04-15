using System;
using System.ComponentModel.DataAnnotations;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class VMViewModel
    {
        public string Memory { get; set; }

        public string VM { get; set; }

        public string Name { get; set; }

        public string Power { get; set; }

        public int CPU { get; set; }

        public DateTime Insert { get; private set; }

    }
}
