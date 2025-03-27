using static Azure.Core.HttpHeader;
using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class Menu_Items
    {
        [Key]
        [Display(Name = "ID Menu Item")]
        public int MenuItemId { get; set; }

        [Display(Name = "ID Menú")]
        public int MenuID { get; set; }
         
        [Display(Name = "ID Plato")]
        public int? PlatoID { get; set; }

        [Display(Name = "ID Combo")]
        public int? ComboID { get; set; }

        [Display(Name = "ID Promoción")]
        public int? PromocionID { get; set; }

        public virtual Platos Plato { get; set; }
        public virtual Combos Combo { get; set; }
    }
}
