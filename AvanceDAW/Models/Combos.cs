using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Combos
    {
        [Key]
        [Display(Name = "ID Combo")]
        public int ComboID { get; set; } 

        [Display(Name = "Nombre del Combo")]
        [Required(ErrorMessage = "El nombre del combo es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Imagen URL")]
        public string? ImagenURL { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
    }
}
