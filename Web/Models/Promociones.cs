using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    [Table("Promociones")]
    public class Promociones
    {

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "ListaPrecios")]
        public int? ListaPreciosRefId { get; set; }
        [ForeignKey("ListaPreciosRefId")]
        public virtual ListaPrecio? ListaPrecios { get; set; }

        [Display(Name = "MetodoPago")]
        public int? MetodoPagoRefId { get; set; }
        [ForeignKey("MetodoPagoRefId")]
        public virtual MetodoPago? MetodoPago { get; set; }

        [Display(Name = "Variacion")]
        public int? MontoVariacionRefId { get; set; }
        [ForeignKey("MontoVariacionRefId")]
        public virtual MetodoPago? MontoVariacion { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TarifaPrecio { get; set; }

        [Display(Name = "Valido Hasta")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ValidoHasta { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
