using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Roles
    {
        [Key]
        [Display(Name = "ID Rol")]
        public int RolID { get; set; }

        [Display(Name = "Nombre del Rol")]
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        public string Nombre { get; set; }
    } 
}
