using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DTOs
{
    public class EntradaMercanciaDTOS
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "Es obligatorio seleccionar un producto")]
        public int ProductoId { get; set; }

        [Display(Name = "Código")]
        public string? CodigoProducto { get; set; }

        [Display(Name = "Nombre")]
        public string? NombreProducto { get; set; }

        [Display(Name = "Cantidad a Ingresar")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        [Range(1, 10000, ErrorMessage = "La cantidad debe ser mínimo 1")]
        public int Cantidad { get; set; }

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        public required string Proveedor { get; set; }

        [Display(Name = "Fecha Entrada")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        [DataType(DataType.DateTime)]
        public DateTime FechaEntrada { get; set; }

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }
    }
}
