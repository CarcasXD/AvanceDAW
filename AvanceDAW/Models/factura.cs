using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class factura
    {
        [Key]
        public int id { get; set; }

        public int pedido_id { get; set; }

        public DateTime? fecha { get; set; }

        public decimal? total { get; set; }

        public int? tipopago_id { get; set; }

        public int? empleado_id { get; set; }
    }
}
