using RepairServiceCenterASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.ViewModels.Sortings
{
    public class OrdersSort
    {
        public Order.SortState DateOrderSort { get; private set; }
        public Order.SortState ReturnDateSort { get; private set; }
        public Order.SortState FullNameCustSort { get; private set; }
        public Order.SortState RepModelSort { get; private set; }
        public Order.SortState TypeOfFaultSort { get; private set; }
        public Order.SortState ServicedStoreSort { get; private set; }
        public Order.SortState GuaranteeMarkSort { get; private set; }
        public Order.SortState GuaranteePeriodSort { get; private set; }
        public Order.SortState PriceSort { get; private set; }
        public Order.SortState EmployeeSort { get; private set; }
        public Order.SortState Current { get; private set; }

        public OrdersSort(Order.SortState sortOrder)
        {
            DateOrderSort = sortOrder == Order.SortState.DateOrderAsc ? Order.SortState.DateOrderDesc
                                                                      : Order.SortState.DateOrderAsc;

            ReturnDateSort = sortOrder == Order.SortState.ReturnDateAsc ? Order.SortState.ReturnDateDesc
                                                                      : Order.SortState.ReturnDateAsc;

            FullNameCustSort = sortOrder == Order.SortState.FullNameCustAsc ? Order.SortState.FullNameCustDesc
                                                                      : Order.SortState.FullNameCustAsc;

            RepModelSort = sortOrder == Order.SortState.RepModelAsc ? Order.SortState.RepModelDesc
                                                                    : Order.SortState.RepModelAsc;

            TypeOfFaultSort = sortOrder == Order.SortState.TypeOfFaultAsc ? Order.SortState.TypeOfFaultDesc
                                                                          : Order.SortState.TypeOfFaultAsc;

            ServicedStoreSort = sortOrder == Order.SortState.ServiceStoreAsc ? Order.SortState.ServiceStroeDesc
                                                                             : Order.SortState.ServiceStoreAsc;

            GuaranteeMarkSort = sortOrder
                == Order.SortState.GuaranteeMarkAsc ? Order.SortState.GuaranteeMarkDesc
                                                      : Order.SortState.GuaranteeMarkAsc;

            GuaranteePeriodSort = sortOrder
                == Order.SortState.GuaranteePeriodAsc ? Order.SortState.GuaranteePeriodDesc
                                                      : Order.SortState.GuaranteePeriodAsc;

            PriceSort = sortOrder == Order.SortState.PriceAsc ? Order.SortState.PriceDesc
                                                              : Order.SortState.PriceAsc;

            EmployeeSort = sortOrder == Order.SortState.EmployeeAsc ? Order.SortState.EmployeeDesc
                                                                    : Order.SortState.EmployeeAsc;

            Current = sortOrder;
        }
    }
}
