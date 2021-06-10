using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class ResponseVueloDto
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public int TipoError { get; set; }
        public List<CoordenadaVuelo> CoordenadasFinales { get; set; } = new List<CoordenadaVuelo>();
    }
}
