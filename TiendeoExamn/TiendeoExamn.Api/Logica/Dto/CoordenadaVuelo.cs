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
        [Range(1, double.MaxValue, ErrorMessage = "La punto X de la coordenada inicial debe ser mayor de 1")]
        public int PuntoX { get; set; }
        [Required(ErrorMessage = "Debe ingresar la posicion Y de la coordenada inicial")]
        [Range(1, double.MaxValue, ErrorMessage = "La punto Y de la coordenada inicial debe ser mayor de 1")]

        public int PuntoY { get; set; }
        [Required(ErrorMessage = "Debe ingresar la dirección de la coordenada inicial")]
        [RegularExpression(@"[N,S,O,E]", ErrorMessage = "Debe ingresar un direccion valida, se acepta: N,S,E,O")]
        public string Direccion { get; set; }
    }
}
