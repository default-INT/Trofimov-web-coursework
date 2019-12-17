using System.ComponentModel.DataAnnotations;

namespace RepairServiceCenterASP.Models
{
    [Display(Name = "Запчасть")]
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
        [Display(Name = "Ремонтируемая модель")]
        public int? RepairedModelId { get; set; }

        [Display(Name = "Ремонтируемая модель")]
        public virtual RepairedModel RepairedModel { get; set; }

        [Display(Name = "Вид неисправности")]
        public int TypeOfFaultId { get; set; }
        [Display(Name = "Вид неисправности")]
        public virtual TypeOfFault TypeOfFault { get; set; }
    }
}
