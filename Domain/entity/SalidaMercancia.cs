using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entity
{
    public class SalidaMercancia
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public required string Motivo { get; set; }
        public required string Destino { get; set; }
        public DateTime FechaSalida { get; set; }
    }
}
