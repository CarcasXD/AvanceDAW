using Microsoft.EntityFrameworkCore;

namespace AvanceDAW.Models
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions options) : base(options) 
        {

        }
    }
}
