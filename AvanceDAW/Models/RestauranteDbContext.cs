using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using System.Data;

namespace AvanceDAW.Models
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Platos> Platos { get; set; } 
        public DbSet<Combos> Combos { get; set; }
        public DbSet<Promociones> Promociones { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<MetodosPago> MetodosPago { get; set; }
        public DbSet<ESTADO_PEDIDO> ESTADO_PEDIDO { get; set; }
        public DbSet<PEDIDO> PEDIDO { get; set; }
        public DbSet<DETALLE_PEDIDO> DETALLE_PEDIDO { get; set; }
        public DbSet<tipopago> tipoPago { get; set; }
        public DbSet<factura> factura { get; set; }
        public DbSet<detallefactura> detallefactura { get; set; }
        public DbSet<Menu_Items> Menu_Items { get; set; }
        public DbSet<PlatosCombos> PlatosCombos { get; set; }
        public DbSet<Promociones_Items> Promociones_Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la clave compuesta para la tabla 'PlatosCombos'
            modelBuilder.Entity<PlatosCombos>()
                .HasKey(pc => new { pc.ComboID, pc.PlatoID });  // Clave primaria compuesta

            modelBuilder.Entity<DETALLE_PEDIDO>()
                .HasKey(dp => new { dp.ID_PEDIDO, dp.ID_MENU });  // Clave primaria compuesta

            // Configurar la relación con MESAS
            modelBuilder.Entity<PEDIDO>()
                .HasOne(p => p.Mesa)
                .WithMany()  // Relación uno a muchos
                .HasForeignKey(p => p.ID_MESA)
                .OnDelete(DeleteBehavior.Restrict);  // Ajusta el comportamiento de eliminación si es necesario

            // Configurar la relación con ESTADO_PEDIDO
            modelBuilder.Entity<PEDIDO>()
                .HasOne(p => p.EstadoPedido)
                .WithMany()  // Relación uno a muchos
                .HasForeignKey(p => p.ID_ESTADOPEDIDO)
                .OnDelete(DeleteBehavior.Restrict);  // Ajusta el comportamiento de eliminación si es necesario

            // Configurar la relación de PEDIDO con Mesero (Empleado)
            modelBuilder.Entity<PEDIDO>()
                .HasOne(p => p.Mesero)  // Relación con Empleados (Mesero)
                .WithMany()  // El mesero puede estar asociado con muchos pedidos
                .HasForeignKey(p => p.ID_MESERO) // La clave foránea es ID_MESERO
                .OnDelete(DeleteBehavior.SetNull); // Configurar el comportamiento en caso de eliminación

            // Configurar la tabla 'PEDIDO'
            modelBuilder.Entity<PEDIDO>()
                .ToTable("PEDIDO");  // Especificar el nombre exacto de la tabla

            base.OnModelCreating(modelBuilder);
        }


    }
}
    

