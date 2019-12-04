using RepairServiceCenterASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.ViewModels
{
    public class ServicedStoresViewModel
    {
        public IEnumerable<ServicedStore> ServicedStores { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
