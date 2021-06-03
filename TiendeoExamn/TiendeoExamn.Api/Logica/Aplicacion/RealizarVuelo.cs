using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendeoExamn.Api.Logica.Dto;
using TiendeoExamn.Api.Logica.Constantes;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace TiendeoExamn.Api.Logica.Aplicacion
{
    public class RealizarVuelo: IRealizarVuelo
    {
        private readonly IConfiguration configuration;
        private readonly List<AccionesVueloDto> accionesVueloDto;
        private readonly List<PlanoCartesianoEntity> planoCartesianoEntity;
        public RealizarVuelo(IConfiguration configuration, IOptions<List<AccionesVueloDto>> optionsAccionesVuelo, IOptions<List<PlanoCartesianoEntity>> optionsPlanoCartesiano)
        {
            this.configuration = configuration;
            this.accionesVueloDto = optionsAccionesVuelo.Value;
            this.planoCartesianoEntity = optionsPlanoCartesiano.Value;

        }
        public List<CoordenadaVuelo> CalcularVuelo(List<InstruccionVueloDto> instrucionesVuelo)
        {
            var coordenadasFinales = new List<CoordenadaVuelo>();

            foreach(var instruccion in instrucionesVuelo)
            {
                var coordenadaActual = instruccion.CoordenadaVuelo;
                var gradoActual = planoCartesianoEntity.Find(x => x.Direccion == coordenadaActual.Direccion).Valor;

                foreach (var accion in instruccion.Acciones)
                {
                    var gradoEquivalenteAccion = accionesVueloDto.Find(x => x.Codigo == accion).Valor;
                    var gradoFinal = gradoActual + gradoEquivalenteAccion;

                    gradoActual = gradoFinal == 360 ? 0 : gradoFinal;
                }

                var direccionFinal = planoCartesianoEntity.Find(x => x.Valor == gradoActual).Direccion;
                coordenadaActual.Direccion = direccionFinal;

                coordenadasFinales.Add(coordenadaActual);
            }
            return coordenadasFinales;
        }
    }
}