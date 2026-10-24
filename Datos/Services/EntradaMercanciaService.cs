using Datos.DTOs;
using Datos.Interfaces;
using Domain.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Services
{
    public class EntradaMercanciaService
    {
        private readonly IEntradaMercancia _Repo;
        private readonly IProducto _productoRepo;

        public EntradaMercanciaService(IEntradaMercancia repo, IProducto productoRepo)
        {
            _Repo = repo;
            _productoRepo = productoRepo;
        }

        public async Task<IEnumerable<EntradaMercanciaDTOS>> ListarEntradas()
        {
            var list = await _Repo.ListarEntradasAsync();
            return list.Select(pi => new EntradaMercanciaDTOS
            {
                Id = pi.Id,
                ProductoId = pi.ProductoId,
                CodigoProducto = pi.CodigoProducto,
                NombreProducto = pi.NombreProducto,
                Cantidad = pi.Cantidad,
                Proveedor = pi.Proveedor,
                FechaEntrada = pi.FechaEntrada,
                Observaciones = pi.Observaciones
            });
        }

        public async Task InsertarEntrada(EntradaMercanciaDTOS dt)
        {
            var prod = await _productoRepo.ObtenerProductoPorIdAsync(dt.ProductoId);
            if (prod != null)
            {
                dt.CodigoProducto = prod.Codigo;
                dt.NombreProducto = prod.Nombre;
                prod.Stock += dt.Cantidad;
                await _productoRepo.ActualizarProductoAsync(prod);
            }

            var nu = new EntradaMercancia
            {
                Id = dt.Id,
                ProductoId = dt.ProductoId,
                CodigoProducto = dt.CodigoProducto ?? "",
                NombreProducto = dt.NombreProducto ?? "",
                Cantidad = dt.Cantidad,
                Proveedor = dt.Proveedor,
                FechaEntrada = dt.FechaEntrada == default ? DateTime.Now : dt.FechaEntrada,
                Observaciones = dt.Observaciones
            };
            await _Repo.InsertarEntradaAsync(nu);
        }
    }
}
