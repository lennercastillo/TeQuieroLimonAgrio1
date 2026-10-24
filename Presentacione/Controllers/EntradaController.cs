using Datos.DTOs;
using Datos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacione.Controllers
{
    public class EntradaController : Controller
    {
        private readonly EntradaMercanciaService _entradaService;
        private readonly ProductoService _productoService;

        public EntradaController(
            EntradaMercanciaService entradaService,
            ProductoService productoService)
        {
            _entradaService = entradaService;
            _productoService = productoService;
        }

        // RF04: Listado de historial de entradas
        public async Task<IActionResult> Index()
        {
            var entradas = await _entradaService.ListarEntradas();
            return View(entradas);
        }

        // RF04: Registrar entradas de mercancía (GET)
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var productos = await _productoService.ListarProductos();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            var modelo = new EntradaMercanciaDTOS
            {
                ProductoId = productos.FirstOrDefault()?.Id ?? 0,
                Cantidad = 1,
                Proveedor = "",
                FechaEntrada = DateTime.Now
            };

            return View(modelo);
        }

        // RF04: Registrar entradas de mercancía (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(EntradaMercanciaDTOS modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.FechaEntrada = DateTime.Now;
                await _entradaService.InsertarEntrada(modelo);
                TempData["Mensaje"] = "Entrada de mercancía registrada y stock actualizado con éxito.";
                return RedirectToAction(nameof(Index));
            }

            var productos = await _productoService.ListarProductos();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre", modelo.ProductoId);
            return View(modelo);
        }
    }
}
