using System;
using System.ComponentModel.DataAnnotations;

namespace RepairServiceCenterASP.Models
{
    public class Order
    {
        public enum SortState
        {
            DateOrderAsc, DateOrderDesc,
            ReturnDateAsc, ReturnDateDesc,
            FullNameCustAsc, FullNameCustDesc,
            RepModelAsc, RepModelDesc,
            TypeOfFaultAsc, TypeOfFaultDesc,
            ServiceStoreAsc, ServiceStoreDesc,
            GuaranteeMarkAsc, GuaranteeMarkDesc,
            GuaranteePeriodAsc, GuaranteePeriodDesc,
            PriceAsc, PriceDesc,
            EmployeeAsc, EmployeeDesc
        }

        [Display(Name = "Код")]
        public int OrderId { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime DateOrder { get; set; }
        [Display(Name = "Дата возврата")]
        public DateTime ReturnDate { get; set; }
        [Display(Name = "Полное имя заказчика")]
        public string FullNameCustumer { get; set; }
        [Display(Name = "Ремонтируемая модель")]
        public int? RepairedModelId { get; set; }
        [Display(Name = "Тип повреждения")]
        public int? TypeOfFaultId { get; set; }
        [Display(Name = "Магазин")]
        public int? ServicedStoreId { get; set; }
        [Display(Name = "Гарантия")]
        public bool? GuaranteeMark { get; set; }
        [Display(Name = "Срок гарантии")]
        public int GuaranteePeriod { get; set; }
        [Display(Name = "Стоимость заказа")]
        public double Price { get; set; }
        [Display(Name = "Сотрудник")]
        public int? EmployeeId { get; set; }

        public virtual RepairedModel RepairedModel { get; set; }
        public virtual TypeOfFault TypeOfFault { get; set; }
        public virtual ServicedStore ServicedStore { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
