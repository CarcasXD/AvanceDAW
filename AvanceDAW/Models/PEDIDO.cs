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

        // Relación con la tabla MESAS
        public virtual Mesas Mesa { get; set; }

        // Relación con la tabla EMPLEADOS (Mesero)
        public virtual Empleados Mesero { get; set; }

        // Relación con la tabla ESTADO_PEDIDO
        public virtual ESTADO_PEDIDO EstadoPedido { get; set; }

        // Relación con los detalles de los pedidos (DETALLE_PEDIDO)
        public virtual ICollection<DETALLE_PEDIDO> DetallePedidos { get; set; }
    }
}

