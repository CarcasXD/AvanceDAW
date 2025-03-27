using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Menu
    {
        [Key]
        [Display(Name = "ID Menú")]
        public int MenuID { get; set; }

        [Display(Name = "Nombre del Menú")]
        [Required(ErrorMessage = "El nombre del menú es obligatorio")]
        public string Nombre { get; set; }
         
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
    }
}
