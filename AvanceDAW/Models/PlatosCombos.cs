using static Azure.Core.HttpHeader;
using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class PlatosCombos
    {
        [Key]
        [Display(Name = "ID Combo")]
        public int ComboID { get; set; }

        [Key]
        [Display(Name = "ID Plato")]
        public int PlatoID { get; set; }

    }
}
