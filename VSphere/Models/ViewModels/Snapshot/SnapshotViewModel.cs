using System;
using System.Collections.Generic;
using VSphere.Models.Base;

namespace VSphere.Models
{
    public class SnapshotViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string VmName { get; set; }

        public string CreatedDate { get; set; }

        public bool MemorySnapshot { get; set; }

        public List<SnapshotViewModel> GetAll()
        {
            var list = new List<SnapshotViewModel>();

            var item1 = new SnapshotViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Upgrade SO",
                VmName = "LAB-LL-AD01",
                CreatedDate = "21/07/2020",
                MemorySnapshot = true
            };

            var item2 = new SnapshotViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Patches",
                VmName = "LAB-LL-ESXi02",
                CreatedDate = "13/08/2020",
                MemorySnapshot = false
            };

            list.Add(item1);
            list.Add(item2);

            return list;
        }

    }
}
