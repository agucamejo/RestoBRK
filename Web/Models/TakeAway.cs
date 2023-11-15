using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    [Table("TakeAway")]
    public class TakeAway
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

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? HorarioEntrega { get; set; } = DateTime.Now;

        [Display(Name = "Promoción")]
        public int? PromocionRefId { get; set; }
        [ForeignKey("PromocionRefId")]
        public virtual Promociones? Promocion { get; set; }

        [Display(Name = "NombreCliente")]
        [StringLength(50)]
        public string? NombreCliente { get; set; }

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

        public TakeAway()
        {
            Productos = new List<ProductosPedido>();
        }

    }
}
