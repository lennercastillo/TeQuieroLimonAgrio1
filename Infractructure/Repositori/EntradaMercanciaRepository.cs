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
    public class EntradaMercanciaRepository : IEntradaMercancia
    {
        private readonly Database? _data;
        private static readonly List<EntradaMercancia> _entradas = new()
        {
            new EntradaMercancia { Id = 1, ProductoId = 1, CodigoProducto = "001", NombreProducto = "Taladro", Cantidad = 5, Proveedor = "Proveedor Central", FechaEntrada = DateTime.Now, Observaciones = "Compra inicial" },
            new EntradaMercancia { Id = 2, ProductoId = 2, CodigoProducto = "002", NombreProducto = "Martillo", Cantidad = 10, Proveedor = "Ferretería S.A.", FechaEntrada = DateTime.Now, Observaciones = "Reabastecimiento" }
        };

        public EntradaMercanciaRepository(Database? DT = null)
        {
            _data = DT;
        }

        public async Task<IEnumerable<EntradaMercancia>> ListarEntradasAsync()
        {
            await Task.Yield();
            return _entradas.ToList();
        }

        public async Task InsertarEntradaAsync(EntradaMercancia entrada)
        {
            await Task.Yield();
            entrada.Id = _entradas.Count > 0 ? _entradas.Max(e => e.Id) + 1 : 1;
            _entradas.Add(entrada);
        }
    }
}
