using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendeoExamn.Api.Logica.Aplicacion;
using TiendeoExamn.Api.Logica.Dto;

namespace TiendeoExamn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlForestalController : ControllerBase
    {
        private readonly IRealizarVuelo realizarVuelo;

        public ControlForestalController(IRealizarVuelo realizarVuelo)
        {
            this.realizarVuelo = realizarVuelo;
        }

        [HttpPost]
        public async Task<ActionResult> LanzarDron(PeticionDto peticionDto)
        {
            var responseDto = realizarVuelo.CalcularVuelo(peticionDto);

            if (!responseDto.Resultado)
            {
                return BadRequest(responseDto);
            }

            return Ok(responseDto);
        }
    }
}
