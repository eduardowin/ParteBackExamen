using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class InstruccionVueloDto
    {
        [Required(ErrorMessage = "Debe ingresar la coordenada inicial de vuelo")]
        public CoordenadaVuelo CoordenadaVuelo { get; set; }
        [ListaAccionesEsValida(ErrorMessage = "Debe ingresar una accion valida, se acepta: L,R,M")]
        public List<string> Acciones { get; set; } = new List<string>();
    }

    public class ListaAccionesEsValida: ValidationAttribute
    {
        public override bool IsValid(object acciones)
        {
            var regex = new Regex(@"L|R|M");
            foreach (string accion in acciones as IList<string>)
            {
                if (!regex.IsMatch(accion))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
