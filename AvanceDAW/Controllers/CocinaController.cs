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

        public IActionResult Index()
        {
            /*
            // Obtener las órdenes solicitadas y mapearlas a 'OrdenModel'
            var ordenesSolicitadas = _context.PEDIDO
                .Where(p => p.EstadoPedido.ESTADO_NOMBRE == "Solicitado")
                .Select(p => new OrdenModel  // Usamos 'OrdenModel' directamente
                {
                    ID_PEDIDO = p.ID_PEDIDO,
                    NumeroMesa = p.Mesa.NumeroMesa
                })
                .ToList();  // Asegúrate de usar ToList() para obtener una lista

            // Obtener las órdenes en proceso y mapearlas a 'OrdenModel'
            var ordenesEnProceso = _context.PEDIDO
                .Where(p => p.EstadoPedido.ESTADO_NOMBRE == "En proceso")
                .Select(p => new OrdenModel  // Usamos 'OrdenModel' directamente
                {
                    ID_PEDIDO = p.ID_PEDIDO,
                    NumeroMesa = p.Mesa.NumeroMesa
                })
                .ToList();  // Asegúrate de usar ToList() para obtener una lista

            // Crear el modelo con las órdenes solicitadas y en proceso
            var modelo = new CocinaViewModel
            {
                Solicitadas = ordenesSolicitadas,
                EnProceso = ordenesEnProceso
            };

            // Pasar el modelo a la vista
            return View(modelo);*/
            
            
            return View();
        }

    }

    // Crear un modelo para enviar a la vista
    /*
    public class CocinaViewModel
    {
        public List<dynamic> Solicitadas { get; set; }
        public List<dynamic> EnProceso { get; set; }
    }
    public class Orden
    {
        public int ID_PEDIDO { get; set; }
        public int NumeroMesa { get; set; }
    }*/
}
