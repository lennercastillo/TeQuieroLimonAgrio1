using Datos.Interfaces;
using Domain.entity;
using Infractructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractructure.Repositori
{
    public class ProductoRepository : IProducto
    {
        private readonly Database? _data;
        private static readonly List<Producto> _productos = new()
        {
            new Producto { Id = 1, Codigo = "001", Nombre = "Taladro", Categoria = "Herramientas", Descripcion = "Taladro percutor", Precio = 2500, Stock = 8, FechaRegistro = DateTime.Now },
            new Producto { Id = 2, Codigo = "002", Nombre = "Martillo", Categoria = "Herramientas", Descripcion = "Martillo de uña", Precio = 350, Stock = 15, FechaRegistro = DateTime.Now },
            new Producto { Id = 3, Codigo = "003", Nombre = "Sierra Circular", Categoria = "Herramientas", Descripcion = "Sierra eléctrica", Precio = 4200, Stock = 5, FechaRegistro = DateTime.Now },
            new Producto { Id = 4, Codigo = "004", Nombre = "Destornillador", Categoria = "Herramientas", Descripcion = "Destornillador plano", Precio = 120, Stock = 20, FechaRegistro = DateTime.Now }
        };

        public ProductoRepository(Database? DT = null)
        {
            _data = DT;
        }

        public async Task<IEnumerable<Producto>> ListarProductosAsync()
        {
            await Task.Yield();
            return _productos.ToList();
        }

        public async Task<Producto?> ObtenerProductoPorIdAsync(int id)
        {
            await Task.Yield();
            return _productos.FirstOrDefault(p => p.Id == id);
        }

        public async Task InsertarProductoAsync(Producto producto)
        {
            await Task.Yield();
            producto.Id = _productos.Count > 0 ? _productos.Max(p => p.Id) + 1 : 1;
            _productos.Add(producto);
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            await Task.Yield();
            var item = _productos.FirstOrDefault(p => p.Id == producto.Id);
            if (item != null)
            {
                item.Codigo = producto.Codigo;
                item.Nombre = producto.Nombre;
                item.Categoria = producto.Categoria;
                item.Descripcion = producto.Descripcion;
                item.Precio = producto.Precio;
                item.Stock = producto.Stock;
            }
        }

        public async Task EliminarProductoAsync(int id)
        {
            await Task.Yield();
            _productos.RemoveAll(p => p.Id == id);
        }

        public async Task<IEnumerable<Producto>> BuscarProductosAsync(string termino)
        {
            await Task.Yield();
            if (string.IsNullOrWhiteSpace(termino))
            {
                return _productos.ToList();
            }

            var t = termino.Trim().ToLower();
            return _productos.Where(p =>
                p.Codigo.ToLower().Contains(t) ||
                p.Nombre.ToLower().Contains(t)
            ).ToList();
        }
    }
}
