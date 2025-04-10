using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class detallefactura
    {
        [Key]
        [Display(Name = "ID Factura")]
        public int id { get; set; }

        [Display(Name = "ID Plato")]
        public int? plato_id { get; set; }

        [Display(Name = "ID Combo")] 
        public int? combo_id { get; set; }

        [Display(Name = "ID Factura")]
        public int? factura_id { get; set; }

        [Display(Name = "Subtotal")]
        public decimal? subtotal { get; set; }
    }
}
