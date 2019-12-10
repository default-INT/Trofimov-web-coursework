using RepairServiceCenterASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.ViewModels.Sortings
{
    public class TypesOfFaultsSort
    {
        public TypeOfFault.SortState RepairedModelSort { get; set; }
        public TypeOfFault.SortState NameSort { get; set; }
        public TypeOfFault.SortState MethodRepairSort { get; set; }
        public TypeOfFault.SortState WorkPriceSort { get; set; }
        public TypeOfFault.SortState Current { get; private set; }

        public TypesOfFaultsSort(TypeOfFault.SortState sortOrder)
        {
            RepairedModelSort = sortOrder == TypeOfFault.SortState.RepairedModelAsc ? TypeOfFault.SortState.RepairedModelDesc
                                                                                    : TypeOfFault.SortState.RepairedModelAsc;

            NameSort = sortOrder == TypeOfFault.SortState.NameAsc ? TypeOfFault.SortState.NameDesc
                                                                  : TypeOfFault.SortState.NameAsc;

            MethodRepairSort = sortOrder == TypeOfFault.SortState.MethodRepairAsc ? TypeOfFault.SortState.MethodRepairDesc
                                                                                  : TypeOfFault.SortState.MethodRepairAsc;

            WorkPriceSort = sortOrder == TypeOfFault.SortState.WorkPriceAsc ? TypeOfFault.SortState.WorkPriceDesc
                                                                            : TypeOfFault.SortState.WorkPriceAsc;

            Current = sortOrder;
        }
    }
}
