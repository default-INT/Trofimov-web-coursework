using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RepairServiceCenterASP.Models
{
    public class SparePart
    {
        [Display(Name = "Код")]
        public int SparePartId { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Функции")]
        public string Functions { get; set; }
        [Display(Name = "Стоимость")]
        public double? Price { get; set; }
        [Display(Name = "Id ремонтируемой модели")]
        public int? RepairedModelId { get; set; }

        [Display(Name = "Ремонтируемая модель")]
        public RepairedModel RepairedModel { get; set; }

        [Display(Name = "Id повреждения")]
        public int TypeOfFaultId { get; set; }
        [Display(Name = "Типы повреждений")]
        public TypeOfFault TypeOfFault { get; set; }
    }
}
