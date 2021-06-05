using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendeoExamn.Api.Logica.Dto
{
    public class PlanoCartesianoEntity
    {
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public List<int> Angulos { get; set; }
    }
}
