using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class MetodosPago
    {
        [Key]
        [Display(Name = "ID Método de Pago")]
        public int MetodoID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del método de pago es obligatorio")] 
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }
    }
}
