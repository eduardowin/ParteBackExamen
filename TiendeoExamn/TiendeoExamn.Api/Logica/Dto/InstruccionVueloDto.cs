using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class InstruccionVueloDto
    {
        [Required(ErrorMessage = "Debe ingresar la coordenada inicial de vuelo")]
        public CoordenadaVuelo CoordenadaVuelo { get; set; }
        public List<string> Acciones { get; set; }
    }
}
