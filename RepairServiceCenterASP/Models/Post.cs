using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public double? Money { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Post()
        {
            Employees = new List<Employee>();
        }
    }
}
