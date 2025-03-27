using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Categorias
    {
        [Key]
        [Display(Name = "ID Categoría")]
        public int CategoriaID { get; set; }

        [Display(Name = "Nombre de la Categoría")]
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }
    }
}
