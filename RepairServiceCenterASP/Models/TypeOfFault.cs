using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Models
{
    public class TypeOfFault
    {
        public int TypeOfFaultId { get; set; }
        public int RepairedModelId { get; set; }
        public string Name { get; set; }
        public string MethodRepair { get; set; }
        public double? WorkPrice { get; set; }

        public RepairedModel RepairedModel { get; set; }
        public IEnumerable<SparePart> SpareParts { get; set; }

        public ICollection<Order> Orders { get; set; }

        public TypeOfFault()
        {
            Orders = new List<Order>();
        }
    }
}
