using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    [Table("MetodoPago")]
    public class MetodoPago
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar el nombre del método de pago")]
        [Display(Name = "NombreMetodo")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "VariaciónPago")]
        [StringLength(50)]
        public string? VariaciónPago { get; set; }

        [Display(Name = "MontoVariacion")]
        public int? MontoVariacion { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }
    }
}
