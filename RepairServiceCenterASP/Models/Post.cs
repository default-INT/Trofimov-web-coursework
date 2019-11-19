using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepairServiceCenterASP.Models
{
    public class Post
    {
        [Display(Name = "Код")]
        public int PostId { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Зарплата")]
        public double? Money { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Post()
        {
            Employees = new List<Employee>();
        }
    }
}
