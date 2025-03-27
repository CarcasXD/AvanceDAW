using AvanceDAW.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvanceDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APICocinaController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public APICocinaController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetPendiente")]
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
        [Route("GetProceso")]
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
    }
}
