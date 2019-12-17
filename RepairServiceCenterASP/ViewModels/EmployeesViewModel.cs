using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.ViewModels.Filters;
using RepairServiceCenterASP.ViewModels.Sortings;
using System.Collections.Generic;

namespace RepairServiceCenterASP.ViewModels
{
    public class EmployeesViewModel
    {
        public EmployeesSort EmployeesSort { get; set; }
        public EmployeesFilter EmployeesFilter { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
