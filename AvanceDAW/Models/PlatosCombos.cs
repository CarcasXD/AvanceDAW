using static Azure.Core.HttpHeader;
using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class PlatosCombos
    {
        [Key]
        [Display(Name = "ID Combo")]
        public int ComboID { get; set; }

        
        [Display(Name = "ID Plato")]
        public int PlatoID { get; set; }

        public virtual Combos Combo { get; set; }

        // Relación con la tabla Plato
        public virtual Platos Plato { get; set; }

    }
}
