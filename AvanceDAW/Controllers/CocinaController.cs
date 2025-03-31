using Microsoft.AspNetCore.Mvc;
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
                                  NumeroMesa = m.NumeroMesa
                              }).ToList();


            var enProceso = (from p in _context.PEDIDO
                             join e in _context.ESTADO_PEDIDO on p.ID_ESTADOPEDIDO equals e.ID_ESTADOPEDIDO
                             join m in _context.Mesas on p.ID_MESA equals m.MesaID
                             where e.ESTADO_NOMBRE == "En Preparación"
                             select new PedidoViewModel
                             {
                                 ID_PEDIDO = p.ID_PEDIDO,
                                 NumeroMesa = m.NumeroMesa
                             }).ToList();

            

            ViewData["Solicitadas"] = pendientes;
            ViewData["EnProceso"] = enProceso;

            return View();
        }

    }

}
