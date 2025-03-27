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
        public DbSet<ESTADO_PEDIDO> EstadoPedido { get; set; }
        public DbSet<PEDIDO> Pedidos { get; set; }
        public DbSet<DETALLE_PEDIDO> DetallePedidos { get; set; }
        public DbSet<tipopago> TipoPago { get; set; }
        public DbSet<factura> Facturas { get; set; }
        public DbSet<detallefactura> DetalleFacturas { get; set; }
        public DbSet<Menu_Items> MenuItems { get; set; }
        public DbSet<PlatosCombos> PlatosCombos { get; set; }
        public DbSet<Promociones_Items> PromocionesItems { get; set; }
        }
}
    

