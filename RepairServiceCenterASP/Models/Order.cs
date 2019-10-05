using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime ReturnDate { get; set; }
        public string FullNameCustumer { get; set; }
        public int? RepairedModelId { get; set; }
        public int? TypeOfFaultId { get; set; }
        public int? ServicedStoreId { get; set; }
        public bool? GuaranteeMark { get; set; }
        public int GuaranteePeriod { get; set; }
        public double Price { get; set; }
        public int? EmployeeId { get; set; }

        public RepairedModel RepairedModel { get; set; }
        public TypeOfFault TypeOfFault { get; set; }
        public ServicedStore ServicedStore { get; set; }
        public Employee Employee { get; set; }
    }
}
