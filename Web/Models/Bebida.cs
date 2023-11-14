using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    [Table("Bebida")]
    public class Bebida
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }


        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemBebida { get; set; }

        [Display(Name = "Clasificación")]
        public int? ClasificacionBebidaRefId { get; set; }
        [ForeignKey("ClasificacionBebidaRefId")]
        public virtual ClasificacionBebida? ClasificacionBebida { get; set; }

        [Display(Name = "Tipo")]
        public int? TipoBebidaRefId { get; set; }
        [ForeignKey("TipoBebidaRefId")]
        public virtual TipoBebida? TipoBebida { get; set; }

        [Display(Name = "Tamano")]
        public int? TamanoRefId { get; set; }
        [ForeignKey("TamanoRefId")]
        public virtual Tamano? Tamano { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar el precio.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }
    }
}
