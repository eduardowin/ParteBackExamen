using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class PeticionDto
    {
        public int PerimetroRectanguloBase { get; set; }
        public int PerimetroRectanguloAltura { get; set; }
        public List<InstruccionVueloDto> InstruccionesDto { get; set; }
    }
}
