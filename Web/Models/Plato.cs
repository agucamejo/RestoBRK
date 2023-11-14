using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
        [Table("Plato")]
        public class Plato
        {
            [Key]
            [Column("ID")]
            public int Id { get; set; }


            [Display(Name = "Descripción")]
            [StringLength(50)]
            public string? Descripcion { get; set; }

            [Display(Name = "Imagen")]
            public string? ImagemComida { get; set; }

            [Display(Name = "Clasificación")]
            public int? ClasificacionComidaRefId { get; set; }
            [ForeignKey("ClasificacionComidaRefId")]
            public virtual ClasificacionComida? ClasificacionComida { get; set; }

            [Display(Name = "Disponibilidad")]
            public int? DisponibilidadRefId { get; set; }
            [ForeignKey("DisponibilidadRefId")]
            public virtual Disponibilidad? Disponibilidad { get; set; }

            [Display(Name = "Tipo")]
            public int? TipoComidaRefId { get; set; }
            [ForeignKey("TipoComidaRefId")]
            public virtual TipoComida? TipoComida { get; set; }

            [Display(Name = "Porcion")]
            public int? PorcionRefId { get; set; }
            [ForeignKey("PorcionRefId")]
            public virtual Porcion? Porcion { get; set; }

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
