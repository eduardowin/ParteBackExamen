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

                foreach (var accionInstruccion in instruccion.Acciones)
                {
                    switch (accionInstruccion)
                    {
                        case Constantes.Constantes.AccionesVuelo.Avanzar1:
                            CalcularAvance(ref coordenadaActual);
                            break;
                        default:
                            ObtenerDireccion(ref coordenadaActual, accionInstruccion);
                            break;
                    }
                }

                coordenadasFinales.Add(coordenadaActual);
            }
            return coordenadasFinales;
        }

        private void CalcularAvance(ref CoordenadaVuelo coordenadaActual)
        {
            switch (coordenadaActual.Direccion)
            {
                case Constantes.Constantes.Direcciones.Este:
                    coordenadaActual.PuntoX += 1;
                    break;
                case Constantes.Constantes.Direcciones.Oeste:
                    coordenadaActual.PuntoX -= 1;
                    break;
                case Constantes.Constantes.Direcciones.Norte:
                    coordenadaActual.PuntoY += 1;
                    break;
                case Constantes.Constantes.Direcciones.Sur:
                    coordenadaActual.PuntoY -= 1;
                    break;
            }
        }
        private void ObtenerDireccion(ref CoordenadaVuelo coordenadaActual, string accionInstruccion)
        {
            var direccionActual = coordenadaActual.Direccion;
            var anguloActual = planoCartesianoEntity.Find(x => x.Direccion == direccionActual).Angulos.First();

            var anguloEquivalenteAccion = accionesVueloDto.Find(x => x.Codigo == accionInstruccion).Valor;
            var anguloFinal = anguloActual + anguloEquivalenteAccion;

            var direccionFinal = planoCartesianoEntity.Find(x => x.Angulos.FindAll(y => y == anguloFinal).Count > 0).Direccion;
            coordenadaActual.Direccion = direccionFinal;
        }
    }
}