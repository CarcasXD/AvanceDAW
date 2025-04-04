using AvanceDAW.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
