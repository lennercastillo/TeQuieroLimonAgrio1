using Datos.DTOs;
using Datos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacione.Controllers
{
    public class SalidaController : Controller
    {
        private readonly SalidaMercanciaService _salidaService;
        private readonly ProductoService _productoService;

        public SalidaController(
            SalidaMercanciaService salidaService,
            ProductoService productoService)
        {
            _salidaService = salidaService;
            _productoService = productoService;
        }

        // RF05: Listado de historial de salidas
        public async Task<IActionResult> Index()
        {
            var salidas = await _salidaService.ListarSalidas();
            return View(salidas);
        }

        // RF05: Registrar salidas de mercancía (GET)
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var productos = await _productoService.ListarProductos();
            ViewBag.Productos = new SelectList(productos.Select(p => new
            {
                p.Id,
                NombreConStock = $"{p.Codigo} - {p.Nombre} (Stock Disp: {p.Stock})"
            }), "Id", "NombreConStock");

            var modelo = new SalidaMercanciaDTOS
            {
                ProductoId = productos.FirstOrDefault()?.Id ?? 0,
                Cantidad = 1,
                Motivo = "Venta Regular",
                Destino = "",
                FechaSalida = DateTime.Now
            };

            return View(modelo);
        }

        // RF05: Registrar salidas de mercancía (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(SalidaMercanciaDTOS modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.FechaSalida = DateTime.Now;
                var (exito, mensaje) = await _salidaService.InsertarSalida(modelo);
                if (exito)
                {
                    TempData["Mensaje"] = mensaje;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, mensaje);
            }

            var productos = await _productoService.ListarProductos();
            ViewBag.Productos = new SelectList(productos.Select(p => new
            {
                p.Id,
                NombreConStock = $"{p.Codigo} - {p.Nombre} (Stock Disp: {p.Stock})"
            }), "Id", "NombreConStock", modelo.ProductoId);

            return View(modelo);
        }
    }
}
