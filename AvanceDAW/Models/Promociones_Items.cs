using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Promociones_Items
    {
        [Key]
        [Display(Name = "ID Promoción")]
        public int PromocionID { get; set; }

        [Key]
        [Display(Name = "ID Plato")] 
        public int? PlatoID { get; set; }

        [Key]
        [Display(Name = "ID Combo")]
        public int? ComboID { get; set; }
    }
}
