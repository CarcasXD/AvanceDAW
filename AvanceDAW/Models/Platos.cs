using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Platos
    {
        [Key]
        [Display(Name = "ID Plato")]
        public int PlatoID { get; set; }

        [Display(Name = "Nombre del Plato")]
        [Required(ErrorMessage = "El nombre del plato es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

        [Display(Name = "Imagen URL")]
        public string? ImagenURL { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Categoría")]
        public int? CategoriaID { get; set; }
    }
}
