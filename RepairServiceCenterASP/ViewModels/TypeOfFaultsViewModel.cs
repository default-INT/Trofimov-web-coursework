using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.ViewModels.Filters;
using RepairServiceCenterASP.ViewModels.Sortings;
using System.Collections.Generic;

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
