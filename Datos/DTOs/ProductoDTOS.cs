using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DTOs
{
    public class ProductoDTOS
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        public required string Codigo { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        public required string Nombre { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        public required string Categoria { get; set; }

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        [Range(0, 100000, ErrorMessage = "El stock debe ser 0 o superior")]
        public int Stock { get; set; }

        [Display(Name = "Fecha Registro")]
        [Required(ErrorMessage = "Es obligatorio este campo")]
        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; }
    }
}
