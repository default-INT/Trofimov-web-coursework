using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepairServiceCenterASP.Models
{
    [Display(Name = "Обслуживаемый магазин")]
    public class ServicedStore
    {
        [Display(Name = "Код")]
        public int ServicedStoreId { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ServicedStore()
        {
            Orders = new List<Order>();
        }
    }
}
