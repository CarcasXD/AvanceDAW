using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class ESTADO_PEDIDO
    {
        [Key]
        [Display(Name = "ID Estado Pedido")]
        public int ID_ESTADOPEDIDO { get; set; }

        [Display(Name = "Nombre del Estado")]
        [Required(ErrorMessage = "El nombre del estado es obligatorio")] 
        public string ESTADO_NOMBRE { get; set; }
    }
}
