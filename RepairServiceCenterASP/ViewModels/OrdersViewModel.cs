using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.ViewModels.Filters;
using RepairServiceCenterASP.ViewModels.Sortings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.ViewModels
{
    public class OrdersViewModel
    {
        public OrdersFilter OrdersFilter { get; set; }
        public OrdersSort OrdersSort { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
