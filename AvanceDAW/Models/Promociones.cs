using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Promociones
    {
        [Key]
        [Display(Name = "ID Promoción")]
        public int PromocionID { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        [Display(Name = "Descuento (%)")]
        [Required(ErrorMessage = "El descuento es obligatorio")]
        public decimal Descuento { get; set; }

        [Display(Name = "Imagen URL")]
        public string? ImagenURL { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; }
    }
}
