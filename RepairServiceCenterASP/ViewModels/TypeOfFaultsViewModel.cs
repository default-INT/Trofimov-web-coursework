using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.ViewModels.Filters;
using RepairServiceCenterASP.ViewModels.Sortings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.ViewModels
{
    public class TypeOfFaultsViewModel
    {
        public TypesOfFaultsFilter TypesOfFaultsFilter { get; set; }
        public TypesOfFaultsSort TypesOfFaultsSort { get; set; } 
        public IEnumerable<TypeOfFault> TypeOfFaults { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
