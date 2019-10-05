using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }
        public string Name { get; set; }
        public string Functions { get; set; }
        public double? Price { get; set; }
        public int? RepairedModelId { get; set; }

        public RepairedModel RepairedModel { get; set; }

        public int TypeOfFaultId { get; set; }
        public TypeOfFault TypeOfFault { get; set; }
    }
}
