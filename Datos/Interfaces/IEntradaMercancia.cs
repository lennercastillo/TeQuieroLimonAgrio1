using Domain.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IEntradaMercancia
    {
        Task<IEnumerable<EntradaMercancia>> ListarEntradasAsync();
        Task InsertarEntradaAsync(EntradaMercancia entrada);
    }
}
