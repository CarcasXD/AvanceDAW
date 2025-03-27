using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Mesas
    {
        [Key]
        [Display(Name = "ID Mesa")]
        public int MesaID { get; set; }

        [Display(Name = "Número de Mesa")]
        [Required(ErrorMessage = "El número de mesa es obligatorio")]
        public int NumeroMesa { get; set; }

        [Display(Name = "Capacidad")]
        [Required(ErrorMessage = "La capacidad es obligatoria") ]
        public int Capacidad { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }
    }
}
