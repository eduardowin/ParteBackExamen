using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class PeticionDto
    {
        [Required(ErrorMessage = "Debe ingresar la base del rectangulo")]
        public int PerimetroRectanguloBase { get; set; }
        [Required(ErrorMessage = "Debe ingresar la altura del rectangulo")]
        public int PerimetroRectanguloAltura { get; set; }
        [Required, MinLength(1, ErrorMessage = "Debe ingresar almenos una instruccion de vuelo")]
        public List<InstruccionVueloDto> InstruccionesDto { get; set; }
    }
}
