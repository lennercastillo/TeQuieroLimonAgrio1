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
    public class SalidaMercanciaService
    {
        private readonly ISalidaMercancia _Repo;
        private readonly IProducto _productoRepo;

        public SalidaMercanciaService(ISalidaMercancia repo, IProducto productoRepo)
        {
            _Repo = repo;
            _productoRepo = productoRepo;
        }

        public async Task<IEnumerable<SalidaMercanciaDTOS>> ListarSalidas()
        {
            var list = await _Repo.ListarSalidasAsync();
            return list.Select(pi => new SalidaMercanciaDTOS
            {
                Id = pi.Id,
                ProductoId = pi.ProductoId,
                CodigoProducto = pi.CodigoProducto,
                NombreProducto = pi.NombreProducto,
                Cantidad = pi.Cantidad,
                Motivo = pi.Motivo,
                Destino = pi.Destino,
                FechaSalida = pi.FechaSalida
            });
        }

        public async Task<(bool Exito, string Mensaje)> InsertarSalida(SalidaMercanciaDTOS dt)
        {
            var prod = await _productoRepo.ObtenerProductoPorIdAsync(dt.ProductoId);
            if (prod == null)
            {
                return (false, "El producto seleccionado no existe.");
            }

            if (prod.Stock < dt.Cantidad)
            {
                return (false, $"Existencias insuficientes. Stock actual: {prod.Stock}, cantidad solicitada: {dt.Cantidad}.");
            }

            dt.CodigoProducto = prod.Codigo;
            dt.NombreProducto = prod.Nombre;
            prod.Stock -= dt.Cantidad;
            await _productoRepo.ActualizarProductoAsync(prod);

            var nu = new SalidaMercancia
            {
                Id = dt.Id,
                ProductoId = dt.ProductoId,
                CodigoProducto = dt.CodigoProducto ?? "",
                NombreProducto = dt.NombreProducto ?? "",
                Cantidad = dt.Cantidad,
                Motivo = dt.Motivo,
                Destino = dt.Destino,
                FechaSalida = dt.FechaSalida == default ? DateTime.Now : dt.FechaSalida
            };
            await _Repo.InsertarSalidaAsync(nu);
            return (true, "Salida de mercancía registrada exitosamente.");
        }
    }
}
