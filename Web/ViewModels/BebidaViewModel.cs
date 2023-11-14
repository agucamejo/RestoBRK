using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Models;

namespace Web.ViewModels
{
    public class BebidaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

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

        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }

        [Display(Name = "Imagem Bebida")]
        public IFormFile Imagem { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemBebida { get; set; }
    }
}
