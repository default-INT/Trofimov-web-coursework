using RepairServiceCenterASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.ViewModels
{
    public class RepeiredViewModels
    {
        public IEnumerable<RepairedModel> RepairedModels { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
