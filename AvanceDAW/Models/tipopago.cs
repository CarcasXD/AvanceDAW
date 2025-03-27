using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class tipopago
    {
        [Key]
        [Display(Name = "ID Tipo de Pago")]
        public int ID { get; set; }

        [Display(Name = "Tipo de Pago")]
        [Required(ErrorMessage = "El tipo de pago es obligatorio")]
        public string Tipo { get; set; }
    }
}
