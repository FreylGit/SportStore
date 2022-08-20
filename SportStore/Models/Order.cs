using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public virtual ICollection<CartLine>? Lines { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите имя")]
        [BindNever]
        public bool Shipped { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите первую строчку адреса")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "Введите название региона")]

        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "Введите название страны")]

        public string Country { get; set; }
        public bool GiftWrap { get; set; }

    }
}
