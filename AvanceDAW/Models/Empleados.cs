using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Empleados
    {
        [Key]
        [Display(Name = "ID Empleado")]
        public int EmpleadoID { get; set; }

        [Display(Name = "Foto del Empleado")]
        public string? UrlFotoEmpleado { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contrasena { get; set; }

        [Display(Name = "Rol")]
        public int? RolID { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }
    }
}
