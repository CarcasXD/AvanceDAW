﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AvanceDAW.Models;

using System.Linq;
using AvanceDAW.Controllers; 

namespace AvanceDAW.Controllers
{
    public class CocinaController : Controller
    {
        private readonly RestauranteDbContext _context;

        public CocinaController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PedidosPendientes()
        {
            var lista = (from p in _context.PEDIDO
                         join e in _context.ESTADO_PEDIDO on p.ID_ESTADOPEDIDO equals e.ID_ESTADOPEDIDO
                         join m in _context.Mesas on p.ID_MESA equals m.MesaID
                         where e.ESTADO_NOMBRE == "Pendiente"
                         select new
                         {
                             Numero_pedido = p.ID_PEDIDO,
                             Mesa = m.NumeroMesa
                         }).ToList();

            return Ok();
        }

        [HttpGet]
        public IActionResult PedidosEnProceso()
        {
            var lista = (from p in _context.PEDIDO
                         join e in _context.ESTADO_PEDIDO on p.ID_ESTADOPEDIDO equals e.ID_ESTADOPEDIDO
                         join m in _context.Mesas on p.ID_MESA equals m.MesaID
                         where e.ESTADO_NOMBRE == "En Preparacion"
                         select new
                         {
                             Numero_pedido = p.ID_PEDIDO,
                             Mesa = m.NumeroMesa
                         }).ToList();

            return Ok();
        }

        [HttpGet]
        public IActionResult Index()
        {

            var pendientes = (from p in _context.PEDIDO
                              join e in _context.ESTADO_PEDIDO on p.ID_ESTADOPEDIDO equals e.ID_ESTADOPEDIDO
                              join m in _context.Mesas on p.ID_MESA equals m.MesaID
                              where e.ESTADO_NOMBRE == "Pendiente"
                              select new PedidoViewModel
                              {
                                  ID_PEDIDO = p.ID_PEDIDO,
                                  NumeroMesa = m.NumeroMesa,

                              }).ToList();


            var enProceso = (from p in _context.PEDIDO
                             join e in _context.ESTADO_PEDIDO on p.ID_ESTADOPEDIDO equals e.ID_ESTADOPEDIDO
                             join m in _context.Mesas on p.ID_MESA equals m.MesaID
                             where e.ESTADO_NOMBRE == "En Preparación"
                             select new PedidoViewModel
                             {
                                 ID_PEDIDO = p.ID_PEDIDO,
                                 NumeroMesa = m.NumeroMesa,


                             }).ToList();



            ViewData["Solicitadas"] = pendientes;
            ViewData["EnProceso"] = enProceso;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int idPedido, string nuevoEstado)
        {
            var pedido = _context.PEDIDO.FirstOrDefault(p => p.ID_PEDIDO == idPedido);

            if (pedido != null)
            {
                var estado = _context.ESTADO_PEDIDO.FirstOrDefault(e => e.ESTADO_NOMBRE == nuevoEstado);

                if (estado != null)
                {
                    pedido.ID_ESTADOPEDIDO = estado.ID_ESTADOPEDIDO;
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult verDetalle()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> verDetalle(int idPedido) 
        {
            ViewBag.idPedido = idPedido;

            var detalles = (from dp in _context.DETALLE_PEDIDO
                            join mi in _context.Menu_Items on dp.ID_MENU equals mi.MenuItemId
                            join pl in _context.Platos on mi.MenuItemId equals pl.PlatoID into platos
                            from plato in platos.DefaultIfEmpty()
                            join c in _context.Combos on mi.MenuItemId equals c.ComboID into combos
                            from combo in combos.DefaultIfEmpty()
                            join pr in _context.Promociones on mi.MenuItemId equals pr.PromocionID into promociones
                            from promocion in promociones.DefaultIfEmpty()
                            join ep in _context.ESTADO_PEDIDO on dp.ID_ESTADOPEDIDO equals ep.ID_ESTADOPEDIDO
                            where dp.ID_PEDIDO == idPedido
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
                                EstadoPedido = ep.ESTADO_NOMBRE,
                                Comentarios = dp.DET_COMENTARIOS,
                                idPedido = dp.ID_PEDIDO,
                                idMenu = dp.ID_MENU, 
                                idEstado = dp.ID_ESTADOPEDIDO
                            }).ToList();

            ViewData["detalles"] = detalles;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> cambiarEstadoPlatillo(int idPedido, int idMenu, int idEstado, string Comentarios,
                                                                int Cantidad) 
        {
            Console.WriteLine(idPedido + " " + idMenu + " " + Comentarios + " " + Cantidad);
            
            var detallePedido = (from d in _context.DETALLE_PEDIDO
                                 where d.ID_PEDIDO == idPedido
                                 && d.ID_MENU == idMenu
                                 && d.DET_COMENTARIOS == Comentarios
                                 && d.DET_CANTIDAD == Cantidad
                                 && d.ID_ESTADOPEDIDO == idEstado
                                 select d).FirstOrDefault(); 

            int nuevoEstado = 0;

            if (detallePedido != null) 
            {
                Console.WriteLine($"Detalle encontrado: {detallePedido.ID_PEDIDO}, {detallePedido.ID_MENU}," +
                    $" {detallePedido.DET_COMENTARIOS}, {detallePedido.DET_CANTIDAD}, {detallePedido.ID_ESTADOPEDIDO}");

                if (idEstado == 1)
                {
                    nuevoEstado = 2;
                }
                else if (idEstado == 2) 
                {
                    nuevoEstado = 3;
                }
                else if (idEstado == 3)
                {
                    nuevoEstado = 3;
                }

                if (nuevoEstado == 0)
                {
                    nuevoEstado = 1; 
                }

                Console.WriteLine($"Estado a enviar: {nuevoEstado}");
                detallePedido.ID_ESTADOPEDIDO = nuevoEstado;
                
                await _context.SaveChangesAsync();

            }
            else
            {
                Console.WriteLine("No se encontró el detalle pedido.");
            }


            return RedirectToAction("Index");        
        }

    }

}
