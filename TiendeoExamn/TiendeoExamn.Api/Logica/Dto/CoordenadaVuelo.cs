using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class CoordenadaVuelo
    {
        [Required(ErrorMessage = "Debe ingresar la posicion X de la coordenada inicial")]
        public int PuntoX { get; set; }
        [Required(ErrorMessage = "Debe ingresar la posicion Y de la coordenada inicial")]
        public int PuntoY { get; set; }
        [Required(ErrorMessage = "Debe ingresar la dirección de la coordenada inicial")]
        public string Direccion { get; set; }
    }
}
