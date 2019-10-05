using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Models
{
    public class ServicedStore
    {
        public int ServicedStoreId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ServicedStore()
        {
            Orders = new List<Order>();
        }
    }
}
