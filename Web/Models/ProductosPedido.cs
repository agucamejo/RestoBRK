using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("ProductosPedido")]
    public class ProductosPedido
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "Por favor, seleccione un plato.")]
        public int ListaPrecioRefId { get; set; }
        [ForeignKey("ListaPrecioRefId")]
        public virtual ListaPrecio ? ListaPrecio { get; set; }

        public int? DeliveryId { get; set; }
        [ForeignKey("DeliveryId")]

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
