using Datos.DTOs;
using Datos.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentacione.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // RF01: Mostrar resumen de productos y existencias
        // RF03: Consultar y buscar productos
        public async Task<IActionResult> Index(string? buscar)
        {
            IEnumerable<ProductoDTOS> lista;
            if (!string.IsNullOrWhiteSpace(buscar))
            {
                lista = await _productoService.BuscarProductos(buscar);
                ViewBag.BusquedaActual = buscar;
            }
            else
            {
                lista = await _productoService.ListarProductos();
            }

            var resumen = await _productoService.ObtenerResumen();
            ViewBag.Resumen = resumen;

            return View(lista);
        }

        // RF02: Registrar nuevos productos (GET)
        [HttpGet]
        public IActionResult Crear()
        {
            var modelo = new ProductoDTOS
            {
                Codigo = "",
                Nombre = "",
                Categoria = "Herramientas Generales",
                Precio = 0,
                Stock = 0,
                FechaRegistro = DateTime.Now
            };
            return View(modelo);
        }

        // RF02: Registrar nuevos productos (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ProductoDTOS modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.FechaRegistro = DateTime.Now;
                await _productoService.InsertarProducto(modelo);
                TempData["Mensaje"] = $"¡Producto '{modelo.Nombre}' registrado exitosamente!";
                return RedirectToAction(nameof(Index));
            }

            return View(modelo);
        }
    }
}
