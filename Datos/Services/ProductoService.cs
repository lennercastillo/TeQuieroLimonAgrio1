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
    public class ProductoService
    {
        private readonly IProducto _Repo;

        public ProductoService(IProducto repo)
        {
            _Repo = repo;
        }

        public async Task<IEnumerable<ProductoDTOS>> ListarProductos()
        {
            var list = await _Repo.ListarProductosAsync();
            return list.Select(pi => new ProductoDTOS
            {
                Id = pi.Id,
                Codigo = pi.Codigo,
                Nombre = pi.Nombre,
                Categoria = pi.Categoria,
                Descripcion = pi.Descripcion,
                Precio = pi.Precio,
                Stock = pi.Stock,
                FechaRegistro = pi.FechaRegistro
            });
        }

        public async Task<ProductoDTOS?> ObtenerPorId(int id)
        {
            var p = await _Repo.ObtenerProductoPorIdAsync(id);
            if (p == null) return null;

            return new ProductoDTOS
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                FechaRegistro = p.FechaRegistro
            };
        }

        public async Task InsertarProducto(ProductoDTOS dt)
        {
            var nu = new Producto
            {
                Id = dt.Id,
                Codigo = dt.Codigo,
                Nombre = dt.Nombre,
                Categoria = dt.Categoria,
                Descripcion = dt.Descripcion,
                Precio = dt.Precio,
                Stock = dt.Stock,
                FechaRegistro = dt.FechaRegistro == default ? DateTime.Now : dt.FechaRegistro
            };
            await _Repo.InsertarProductoAsync(nu);
        }

        public async Task ActualizarProducto(ProductoDTOS dt)
        {
            var nu = new Producto
            {
                Id = dt.Id,
                Codigo = dt.Codigo,
                Nombre = dt.Nombre,
                Categoria = dt.Categoria,
                Descripcion = dt.Descripcion,
                Precio = dt.Precio,
                Stock = dt.Stock,
                FechaRegistro = dt.FechaRegistro
            };
            await _Repo.ActualizarProductoAsync(nu);
        }

        public async Task EliminarProducto(int id)
        {
            await _Repo.EliminarProductoAsync(id);
        }

        public async Task<IEnumerable<ProductoDTOS>> BuscarProductos(string termino)
        {
            var list = await _Repo.BuscarProductosAsync(termino);
            return list.Select(pi => new ProductoDTOS
            {
                Id = pi.Id,
                Codigo = pi.Codigo,
                Nombre = pi.Nombre,
                Categoria = pi.Categoria,
                Descripcion = pi.Descripcion,
                Precio = pi.Precio,
                Stock = pi.Stock,
                FechaRegistro = pi.FechaRegistro
            });
        }

        public async Task<ResumenInventarioDTOS> ObtenerResumen()
        {
            var productos = (await _Repo.ListarProductosAsync()).ToList();
            return new ResumenInventarioDTOS
            {
                TotalProductos = productos.Count,
                TotalExistencias = productos.Sum(p => p.Stock),
                ValorInventario = productos.Sum(p => p.Precio * p.Stock),
                ProductosStockBajo = productos.Count(p => p.Stock <= 5)
            };
        }
    }
}
