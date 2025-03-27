using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class PEDIDO
    {
        [Key]
        [Display(Name = "ID Pedido")]
        public int ID_PEDIDO { get; set; }

        [Display(Name = "ID Mesa")]
        public int ID_MESA { get; set; }

        [Display(Name = "ID Mesero")]
        public int ID_MESERO { get; set; }

        [Display(Name = "ID Estado Pedido")]
        public int ID_ESTADOPEDIDO { get; set; }
    }
}
