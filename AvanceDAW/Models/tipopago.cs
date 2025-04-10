using System.ComponentModel.DataAnnotations;

namespace AvanceDAW.Models
{
    public class tipopago
    {
        [Key]
        public int id { get; set; }

        public string tipo { get; set; }
    }
}
