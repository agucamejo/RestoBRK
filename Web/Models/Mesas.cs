using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("Mesas")]
    public class Mesas
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Platos y Bebidas")]
        public virtual List<ProductosPedido>? Productos { get; set; }

        [Display(Name = "Método de Pago")]
        public int MetodoPagoRefId { get; set; }
        [ForeignKey("MetodoPagoRefId")]
        public virtual MetodoPago? MetodoPago { get; set; }

        [Display(Name = "NroMesa")]
        public int NroMesa { get; set; }

        [Display(Name = "Promoción")]
        public int? PromocionRefId { get; set; }
        [ForeignKey("PromocionRefId")]
        public virtual Promociones? Promocion { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioPedido { get; set; }

        public DateTime? FechaRegistro { get; set; } = DateTime.Now;

        [NotMapped]
        public string? ValidationError { get; set; }

        public int NumberOfProductos
        {
            get => Productos.Count;
        }

        public Mesas()
        {
            Productos = new List<ProductosPedido>();
        }

    }
}
