using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class InstruccionVueloDto
    {
        public CoordenadaVuelo CoordenadaVuelo { get; set; }
        public List<string> Acciones { get; set; }
    }
}
