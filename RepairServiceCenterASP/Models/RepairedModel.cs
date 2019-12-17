using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepairServiceCenterASP.Models
{
    [Display(Name = "Ремонтируемая модель")]
    public class RepairedModel
    {
        [Display(Name = "Ремонтируемая модель")]
        public int RepairedModelId { get; set; }
        [Display(Name = "Ремонтируемая модель")]
        public string Name { get; set; }
        [Display(Name = "Тип")]
        public string Type { get; set; }
        [Display(Name = "Производитель")]
        public string Manufacturer { get; set; }
        [Display(Name = "Тех. спецификация")]
        public string TechSpecification { get; set; }
        [Display(Name = "Особенности")]
        public string Features { get; set; }

        public virtual ICollection<SparePart> SpareParts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public RepairedModel()
        {
            SpareParts = new List<SparePart>();
            Orders = new List<Order>();
        }
    }
}
