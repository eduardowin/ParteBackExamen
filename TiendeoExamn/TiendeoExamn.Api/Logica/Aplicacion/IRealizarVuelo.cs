using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendeoExamn.Api.Logica.Dto;

namespace TiendeoExamn.Api.Logica.Aplicacion
{
    public interface IRealizarVuelo
    {
        List<CoordenadaVuelo> CalcularVuelo(List<InstruccionVueloDto> instrucionesVuelo);
        ResponseVueloDto ValidarAreaVuelo(PeticionDto peticionDto);
    }
}
