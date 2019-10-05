using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int? Experience { get; set; }
        public int? PostId { get; set; }

        public Post Post { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Employee()
        {
            Orders = new List<Order>();
        }
    }
}
