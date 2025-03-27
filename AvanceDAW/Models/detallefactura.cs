using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class detallefactura
    {
        [Key]
        [Display(Name = "ID Factura")]
        public int ID { get; set; }

        [Display(Name = "ID Plato")]
        public int? PlatoID { get; set; }

        [Display(Name = "ID Combo")] 
        public int? ComboID { get; set; }

        [Display(Name = "ID Factura")]
        public int FacturaID { get; set; }

        [Display(Name = "Subtotal")]
        public decimal Subtotal { get; set; }
    }
}
