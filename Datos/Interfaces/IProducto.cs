using Domain.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IProducto
    {
        Task<IEnumerable<Producto>> ListarProductosAsync();
        Task<Producto?> ObtenerProductoPorIdAsync(int id);
        Task InsertarProductoAsync(Producto producto);
        Task ActualizarProductoAsync(Producto producto);
        Task EliminarProductoAsync(int id);
        Task<IEnumerable<Producto>> BuscarProductosAsync(string termino);
    }
}
