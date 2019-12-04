using Microsoft.AspNetCore.Mvc.Rendering;
using RepairServiceCenterASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.ViewModels.Filters
{
    public class OrdersFilter
    {
        public DateTime? SelectedDateOrder { get; private set; }
        public string InputFullNameCust { get; private set; }
        public int? SelectedEmployee { get; private set; }
        public SelectList Employees { get; private set; }
        public bool? SelectedGuarantee { get; private set; }

        public OrdersFilter(DateTime? dateOrder, string fullName, List<Employee> employees, int? employee,
                            bool? guarantee)
        {
            employees.Insert(0, new Employee() { EmployeeId = 0, FullName = "Все" });
            Employees = new SelectList(employees, "EmployeeId", "FullName", employee);

            SelectedDateOrder = dateOrder;
            InputFullNameCust = fullName;
            SelectedEmployee = employee;
            SelectedGuarantee = guarantee;
        }
    }
}
