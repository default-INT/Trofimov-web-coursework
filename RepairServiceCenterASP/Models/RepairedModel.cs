using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Models
{
    public class RepairedModel
    {
        public int RepairedModelId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string TechSpecification { get; set; }
        public string Features { get; set; }

        public ICollection<SparePart> SpareParts { get; set; }
        public ICollection<Order> Orders { get; set; }

        public RepairedModel()
        {
            SpareParts = new List<SparePart>();
            Orders = new List<Order>();
        }
    }
}
