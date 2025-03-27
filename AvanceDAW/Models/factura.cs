using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class factura
    {
        [Key]
        [Display(Name = "ID Factura")]
        public int ID { get; set; }

        [Display(Name = "ID Pedido")]
        public int PedidoID { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Tipo de Pago")]
        public int TipoPagoID { get; set; }

        [Display(Name = "Empleado")]
        public int EmpleadoID { get; set; }
    }
}
