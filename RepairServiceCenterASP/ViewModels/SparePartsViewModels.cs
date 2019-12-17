using RepairServiceCenterASP.Models;
using System.Collections.Generic;

namespace RepairServiceCenterASP.ViewModels
{
    public class SparePartsViewModels
    {
        public IEnumerable<SparePart> SpareParts { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
