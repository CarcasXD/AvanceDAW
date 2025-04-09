using AvanceDAW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AvanceDAW.Controllers
{
    public class CajeroController : Controller
    {
        private readonly RestauranteDbContext _context;

        public CajeroController(RestauranteDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mesasabiertas = (from p in _context.PEDIDO
                                 join ep in _context.ESTADO_PEDIDO on p.ID_ESTADOPEDIDO equals ep.ID_ESTADOPEDIDO
                                 join m in _context.Mesas on p.ID_MESA equals m.MesaID
                                 where ep.ESTADO_NOMBRE == "Pendiente" || ep.ESTADO_NOMBRE == "En Preparación"
                                 select new PedidoViewModel
                                 {
                                     ID_PEDIDO = p.ID_PEDIDO,
                                     NumeroMesa = m.NumeroMesa,
                                 }).ToList();

            var mesascerradas = (from p in _context.PEDIDO
                                 join ep in _context.ESTADO_PEDIDO on p.ID_ESTADOPEDIDO equals ep.ID_ESTADOPEDIDO
                                 join m in _context.Mesas on p.ID_MESA equals m.MesaID
                                 where ep.ESTADO_NOMBRE == "Completado"
                                 select new PedidoViewModel
                                 {
                                     ID_PEDIDO = p.ID_PEDIDO,
                                     NumeroMesa = m.NumeroMesa,
                                 }).ToList();

            ViewData["MesasAbiertas"] = mesasabiertas;
            ViewData["MesasCerradas"] = mesascerradas;

            return View();
        }

        public IActionResult VerMesa(int pedidoId, int mesaNum)
        {
            var pedido = (from p in _context.PEDIDO
                          join m in _context.Mesas on p.ID_MESA equals m.MesaID
                          where p.ID_PEDIDO == pedidoId
                          select new
                          {
                              PedidoId = p.ID_PEDIDO,
                              MesaNumero = m.NumeroMesa
                          }).FirstOrDefault();

            var detalles = (from dp in _context.DETALLE_PEDIDO
                            join mi in _context.Menu_Items on dp.ID_MENU equals mi.MenuItemId
                            join pl in _context.Platos on mi.MenuItemId equals pl.PlatoID into platos
                            from plato in platos.DefaultIfEmpty()
                            join c in _context.Combos on mi.MenuItemId equals c.ComboID into combos
                            from combo in combos.DefaultIfEmpty()
                            join pr in _context.Promociones on mi.MenuItemId equals pr.PromocionID into promociones
                            from promocion in promociones.DefaultIfEmpty()
                            where dp.ID_PEDIDO == pedidoId
                            select new
                            {
                                TipoItem = plato != null ? "Plato" :
                                           combo != null ? "Combo" :
                                           promocion != null ? "Promocion" : "Desconocido",
                                NombreItem = plato != null ? plato.Nombre :
                                             combo != null ? combo.Nombre :
                                             promocion != null ? promocion.Descripcion : "N/A",
                                DescripcionItem = plato != null ? plato.Descripcion :
                                                  combo != null ? combo.Descripcion :
                                                  promocion != null ? promocion.Descripcion : "N/A",
                                Cantidad = dp.DET_CANTIDAD,
                                Precio = dp.DET_PRECIO,
                                Subtotal = dp.DET_SUBTOTAL,
                                Comentarios = dp.DET_COMENTARIOS
                            }).ToList();

            if (pedido != null)
            {
                ViewBag.PedidoId = pedido.PedidoId;
                ViewBag.NumeroMesa = pedido.MesaNumero;
            }

            if (detalles != null && detalles.Count > 0)
            {
                ViewBag.DetallePedido = detalles;
            }
            else
            {
                ViewBag.Error = "No se encontraron detalles del pedido.";
            }

            // Retornar la vista
            return View();
        }

        public IActionResult CobrarMesa(int pedidoId, int mesaNum)
        {

            var pedido = (from p in _context.PEDIDO
                          join m in _context.Mesas on p.ID_MESA equals m.MesaID
                          where p.ID_PEDIDO == pedidoId
                          select new
                          {
                              PedidoId = p.ID_PEDIDO,
                              MesaNumero = m.NumeroMesa
                          }).FirstOrDefault();

            var detalles = (from dp in _context.DETALLE_PEDIDO
                            join mi in _context.Menu_Items on dp.ID_MENU equals mi.MenuItemId
                            join pl in _context.Platos on mi.MenuItemId equals pl.PlatoID into platos
                            from plato in platos.DefaultIfEmpty()
                            join c in _context.Combos on mi.MenuItemId equals c.ComboID into combos
                            from combo in combos.DefaultIfEmpty()
                            join pr in _context.Promociones on mi.MenuItemId equals pr.PromocionID into promociones
                            from promocion in promociones.DefaultIfEmpty()
                            where dp.ID_PEDIDO == pedidoId
                            select new
                            {
                                MenuItemId = mi.MenuItemId,
                                TipoItem = plato != null ? "Plato" :
                                           combo != null ? "Combo" :
                                           promocion != null ? "Promocion" : "Desconocido",
                                NombreItem = plato != null ? plato.Nombre :
                                             combo != null ? combo.Nombre :
                                             promocion != null ? promocion.Descripcion : "N/A",
                                DescripcionItem = plato != null ? plato.Descripcion :
                                                  combo != null ? combo.Descripcion :
                                                  promocion != null ? promocion.Descripcion : "N/A",
                                Cantidad = dp.DET_CANTIDAD,
                                Precio = dp.DET_PRECIO,
                                Subtotal = dp.DET_SUBTOTAL,
                                Comentarios = dp.DET_COMENTARIOS
                            }).ToList();

            if (pedido != null)
            {
                ViewBag.PedidoId = pedido.PedidoId;
                ViewBag.NumeroMesa = pedido.MesaNumero;
            }

            if (detalles != null && detalles.Count > 0)
            {
                ViewBag.DetallePedido = detalles;
            }
            else
            {
                ViewBag.Error = "No se encontraron detalles del pedido.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult GenerarFactura(int pedidoId, int mesaNum)
        {

            decimal total = 0;
            var detalles = (from dp in _context.DETALLE_PEDIDO
                            join mi in _context.Menu_Items on dp.ID_MENU equals mi.MenuItemId
                            join pl in _context.Platos on mi.MenuItemId equals pl.PlatoID into platos
                            from plato in platos.DefaultIfEmpty()
                            join c in _context.Combos on mi.MenuItemId equals c.ComboID into combos
                            from combo in combos.DefaultIfEmpty()
                            join pr in _context.Promociones on mi.MenuItemId equals pr.PromocionID into promociones
                            from promocion in promociones.DefaultIfEmpty()
                            where dp.ID_PEDIDO == pedidoId
                            select new
                            {
                                MenuItemId = mi.MenuItemId,
                                TipoItem = plato != null ? "Plato" :
                                           combo != null ? "Combo" :
                                           promocion != null ? "Promocion" : "Desconocido",
                                NombreItem = plato != null ? plato.Nombre :
                                             combo != null ? combo.Nombre :
                                             promocion != null ? promocion.Descripcion : "N/A",
                                Subtotal = dp.DET_SUBTOTAL,
                                PlatoId = plato != null ? plato.PlatoID : (int?)null,
                                ComboId = combo != null ? combo.ComboID : (int?)null
                            }).ToList();

            foreach (var item in detalles)
            {
                total += item.Subtotal;
            }

            var Factura = new factura
            {

                PedidoID = pedidoId,
                Fecha = DateTime.Now,
                Total = total,
                TipoPagoID = 0, 
                EmpleadoID = 0
            };

            _context.factura.Add(Factura);
            _context.SaveChanges(); 

            foreach (var item in detalles)
            {
                var DetalleFactura = new detallefactura
                {
                    FacturaID = Factura.ID,
                    PlatoID = item.PlatoId,
                    ComboID = item.ComboId,
                    Subtotal = item.Subtotal
                };

                _context.detallefactura.Add(DetalleFactura);
            }

            _context.SaveChanges(); 

            return RedirectToAction("FacturaGenerada", new { facturaId = Factura.ID});
        }

        [HttpPost]
        public IActionResult GenerarFactura(int pedidoId, int mesaNum, List<int> selectedItems)
        {
            decimal total = 0;   
            if (selectedItems == null || selectedItems.Count == 0)
            {
                return RedirectToAction("GenerarFactura", new { pedidoId = pedidoId, mesaNum = mesaNum });
            }

            var detallesSeleccionados = (from dp in _context.DETALLE_PEDIDO
                                         join mi in _context.Menu_Items on dp.ID_MENU equals mi.MenuItemId
                                         join pl in _context.Platos on mi.MenuItemId equals pl.PlatoID into platos
                                         from plato in platos.DefaultIfEmpty()
                                         join c in _context.Combos on mi.MenuItemId equals c.ComboID into combos
                                         from combo in combos.DefaultIfEmpty()
                                         join pr in _context.Promociones on mi.MenuItemId equals pr.PromocionID into promociones
                                         from promocion in promociones.DefaultIfEmpty()
                                         where dp.ID_PEDIDO == pedidoId && selectedItems.Contains(mi.MenuItemId)
                                         select new
                                         {
                                             MenuItemId = mi.MenuItemId,
                                             TipoItem = plato != null ? "Plato" :
                                                        combo != null ? "Combo" :
                                                        promocion != null ? "Promocion" : "Desconocido",
                                             NombreItem = plato != null ? plato.Nombre :
                                                          combo != null ? combo.Nombre :
                                                          promocion != null ? promocion.Descripcion : "N/A",
                                             Subtotal = dp.DET_SUBTOTAL,
                                             PlatoId = plato != null ? plato.PlatoID : (int?)null,
                                             ComboId = combo != null ? combo.ComboID : (int?)null
                                         }).ToList();
            foreach (var item in detallesSeleccionados)
            {
                total += item.Subtotal;
            }

            var Factura = new factura
            {
                PedidoID = pedidoId,
                Fecha = DateTime.Now,
                Total = total,
                TipoPagoID = 1, 
                EmpleadoID = 1 
            };

            _context.factura.Add(Factura);
            _context.SaveChanges(); 

            foreach (var item in detallesSeleccionados)
            {
                var DetalleFactura = new detallefactura
                {
                    FacturaID = Factura.ID,
                    PlatoID = item.PlatoId,
                    ComboID = item.ComboId,
                    Subtotal = item.Subtotal
                };

                _context.detallefactura.Add(DetalleFactura);
            }

            _context.SaveChanges(); 

            return RedirectToAction("FacturaGenerada", new { facturaId = Factura.ID });
        }








    }
}
