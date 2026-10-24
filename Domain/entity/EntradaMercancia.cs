using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entity
{
    public class EntradaMercancia
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public required string Proveedor { get; set; }
        public DateTime FechaEntrada { get; set; }
        public string? Observaciones { get; set; }
    }
}
