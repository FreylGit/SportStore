using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SportStore.Models
{
    public class Product
    {
        [Key]
        [Column("ProductId")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Введите наименование товара")]
        [Column("Name", TypeName = "nvarchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Введите положительное значение для цены")]
        [Column("Price")]
        [MaxLength(8)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required]
        [Column("Description", TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Column("Category", TypeName = "nvarchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [Required]
        [Column("CreateDate")]
        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
