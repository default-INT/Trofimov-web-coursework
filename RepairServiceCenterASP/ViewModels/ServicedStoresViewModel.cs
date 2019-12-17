using RepairServiceCenterASP.Models;
using System.Collections.Generic;

namespace RepairServiceCenterASP.ViewModels
{
    public class ServicedStoresViewModel
    {
        public IEnumerable<ServicedStore> ServicedStores { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
