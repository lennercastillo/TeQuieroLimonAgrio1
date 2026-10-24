using Domain.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface ISalidaMercancia
    {
        Task<IEnumerable<SalidaMercancia>> ListarSalidasAsync();
        Task InsertarSalidaAsync(SalidaMercancia salida);
    }
}
