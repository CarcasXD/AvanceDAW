﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<tipopago> TipoPago { get; set; }
        public DbSet<factura> Facturas { get; set; }
        public DbSet<detallefactura> DetalleFacturas { get; set; }
        public DbSet<Menu_Items> MenuItems { get; set; }
        public DbSet<PlatosCombos> PlatosCombos { get; set; }
        public DbSet<Promociones_Items> PromocionesItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la clave compuesta para la tabla 'PlatosCombos'
            modelBuilder.Entity<PlatosCombos>()
                .HasKey(pc => new { pc.ComboID, pc.PlatoID });  // Clave primaria compuesta

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

            // Configurar la tabla 'PEDIDO'
            modelBuilder.Entity<PEDIDO>()
                .ToTable("PEDIDO");  // Especificar el nombre exacto de la tabla

            base.OnModelCreating(modelBuilder);
        }


    }
}
    

