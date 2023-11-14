using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Models;

namespace Web.ViewModels
{
    public class PlatoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

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

        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }

        [Display(Name = "Imagem Comida")]
        public IFormFile Imagem { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemComida { get; set; }
    }
}
