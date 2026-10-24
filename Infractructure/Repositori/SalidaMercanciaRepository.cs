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
    public class SalidaMercanciaRepository : ISalidaMercancia
    {
        private readonly Database? _data;
        private static readonly List<SalidaMercancia> _salidas = new()
        {
            new SalidaMercancia { Id = 1, ProductoId = 1, CodigoProducto = "001", NombreProducto = "Taladro", Cantidad = 2, Motivo = "Venta", Destino = "Cliente Juan", FechaSalida = DateTime.Now },
            new SalidaMercancia { Id = 2, ProductoId = 2, CodigoProducto = "002", NombreProducto = "Martillo", Cantidad = 3, Motivo = "Venta", Destino = "Cliente Pedro", FechaSalida = DateTime.Now }
        };

        public SalidaMercanciaRepository(Database? DT = null)
        {
            _data = DT;
        }

        public async Task<IEnumerable<SalidaMercancia>> ListarSalidasAsync()
        {
            await Task.Yield();
            return _salidas.ToList();
        }

        public async Task InsertarSalidaAsync(SalidaMercancia salida)
        {
            await Task.Yield();
            salida.Id = _salidas.Count > 0 ? _salidas.Max(s => s.Id) + 1 : 1;
            _salidas.Add(salida);
        }
    }
}
