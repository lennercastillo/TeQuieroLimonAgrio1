using Datos.DTOs;
using Datos.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentacione.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductoService _productoService;
        private readonly EntradaMercanciaService _entradaService;
        private readonly SalidaMercanciaService _salidaService;

        public HomeController(
            ProductoService productoService,
            EntradaMercanciaService entradaService,
            SalidaMercanciaService salidaService)
        {
            _productoService = productoService;
            _entradaService = entradaService;
            _salidaService = salidaService;
        }

        public async Task<IActionResult> Index()
        {
            var resumen = await _productoService.ObtenerResumen();
            var entradas = await _entradaService.ListarEntradas();
            var salidas = await _salidaService.ListarSalidas();

            resumen.TotalEntradas = entradas.Count();
            resumen.TotalSalidas = salidas.Count();

            var productos = (await _productoService.ListarProductos()).ToList();
            ViewBag.ProductosBajoStock = productos.Where(p => p.Stock <= 5).ToList();
            ViewBag.UltimosProductos = productos.Take(5).ToList();

            return View(resumen);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
