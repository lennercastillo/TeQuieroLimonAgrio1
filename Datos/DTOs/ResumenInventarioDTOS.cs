using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DTOs
{
    public class ResumenInventarioDTOS
    {
        public int TotalProductos { get; set; }
        public int TotalExistencias { get; set; }
        public decimal ValorInventario { get; set; }
        public int ProductosStockBajo { get; set; }
        public int TotalEntradas { get; set; }
        public int TotalSalidas { get; set; }
    }
}
