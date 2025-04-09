using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class factura
    {
        [Key]
        public int ID { get; set; }

        public int PedidoID { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public int TipoPagoID { get; set; }

        public int EmpleadoID { get; set; }
    }
}
