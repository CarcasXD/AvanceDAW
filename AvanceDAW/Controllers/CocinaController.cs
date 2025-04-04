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
            Console.WriteLine("Llegue");
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


            return View();
        }

    }

}
