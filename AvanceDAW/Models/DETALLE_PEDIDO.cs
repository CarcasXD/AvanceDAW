using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class DETALLE_PEDIDO
    {
        [Display(Name = "ID Pedido")]
        public int ID_PEDIDO { get; set; }
        
        [Display(Name = "ID Menú")]
        public int ID_MENU { get; set; }

        [Display(Name = "Cantidad")]
        public int DET_CANTIDAD { get; set; }

        [Display(Name = "Precio")] 
        public decimal DET_PRECIO { get; set; }

        [Display(Name = "Subtotal")]
        public decimal DET_SUBTOTAL { get; set; }

        [Display(Name = "Comentarios")]
        public string DET_COMENTARIOS { get; set; }
        public virtual Menu_Items MenuItems { get; set; }


        [Display(Name = "ID Estado Pedido")]
        public int ID_ESTADOPEDIDO { get; set; }

        public int EstadoPedidoID_ESTADOPEDIDO { get; set; }
        public int MenuItemsMenuItemId { get; set; }
        public int PEDIDOID_PEDIDO { get; set; }

        public virtual ESTADO_PEDIDO EstadoPedido { get; set; }
    }
}
