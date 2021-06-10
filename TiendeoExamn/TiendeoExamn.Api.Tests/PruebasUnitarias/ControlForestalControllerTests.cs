using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using TiendeoExamn.Api.Logica.Dto;
using TiendeoExamn.Api.Configuracion;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using TiendeoExamn.Api.Controllers;
using TiendeoExamn.Api.Logica.Aplicacion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;

namespace TiendeoExamn.Api.Tests.PruebasUnitarias
{
    [TestClass]
    public class ControlForestalControllerTests
    {

        private WebApplicationFactory<Startup> _factory;

        public ControlForestalControllerTests()
        {
        }
        [TestMethod]
        public async Task Post_SiLaDireccionInicialNoEsValida_SeNosRetornaUn400()
        {
            //// preparación
            _factory = new WebApplicationFactory<Startup>();
            var client = _factory.CreateClient();

            var peticionDto = new PeticionDto
            {
                PerimetroRectanguloAltura = 5,
                PerimetroRectanguloBase = 5
            };
            var instruccion = new InstruccionVueloDto
            {
                Acciones = new List<string> { "L" },
                CoordenadaVuelo = new CoordenadaVuelo { PuntoX = 3, PuntoY = 3, Direccion = "Z" }// Validará esta direccion
            };

            peticionDto.InstruccionesDto.Add(instruccion);

            //// prueba
            var response = await client.PostAsync("/api/ControlForestal", new StringContent(JsonConvert.SerializeObject(peticionDto), System.Text.Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();

            var existeValidacionDireccion = result.Contains("Debe ingresar un direccion valida, se acepta: N,S,E,O");
            //// Verificación
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(true, existeValidacionDireccion);

        }

        [TestMethod]
        public async Task Post_SiLasAccionesNoSonValidas_SeNosRetornaUn400()
        {
            //// preparación
            _factory = new WebApplicationFactory<Startup>();
            var client = _factory.CreateClient();

            var peticionDto = new PeticionDto
            {
                PerimetroRectanguloAltura = 5,
                PerimetroRectanguloBase = 5
            };
            var instruccion = new InstruccionVueloDto
            {
                Acciones = new List<string> { "L", "Z", "Z", "Z", "Z", "Z", "Z" }, // Validará este valor
                CoordenadaVuelo = new CoordenadaVuelo { PuntoX = 3, PuntoY = 3, Direccion = "E" }
            };

            peticionDto.InstruccionesDto.Add(instruccion);

            //// prueba
            var response = await client.PostAsync("/api/ControlForestal", new StringContent(JsonConvert.SerializeObject(peticionDto), System.Text.Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            var existeValidacionAcciones = result.Contains("Debe ingresar una accion valida, se acepta: L,R,M");
            
            //// Verificación
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(true, existeValidacionAcciones);

        }

        [TestMethod]
        public async Task Post_SiElAreaDeVueloDelDronPorSusAccionesNoEsValida_SeNosRetornaUn400()
        {
            //// preparación
            _factory = new WebApplicationFactory<Startup>();
            var client = _factory.CreateClient();

            var peticionDto = new PeticionDto
            {
                PerimetroRectanguloAltura = 5,
                PerimetroRectanguloBase = 5
            };
            var instruccion = new InstruccionVueloDto
            {
                Acciones = new List<string> { "L", "M", "M", "M", "M", "M", "M" }, // Aqui validará lo que el dron avance
                CoordenadaVuelo = new CoordenadaVuelo { PuntoX = 3, PuntoY = 3, Direccion = "E" }
            };

            peticionDto.InstruccionesDto.Add(instruccion);

            //// prueba
            var response = await client.PostAsync("/api/ControlForestal", new StringContent(JsonConvert.SerializeObject(peticionDto), System.Text.Encoding.UTF8, "application/json"));

            var result = JsonConvert.DeserializeObject<ResponseVueloDto>(
                await response.Content.ReadAsStringAsync());
            //// Verificación
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(1, result.TipoError);

        }

        [TestMethod]
        public async Task Post_SiLosValoresDeVueloIngresadoSonValidos_Caso01_SeNosRetornaUn200()
        {
            //// preparación
            _factory = new WebApplicationFactory<Startup>();
            var client = _factory.CreateClient();

            var peticionDto = new PeticionDto
            {
                PerimetroRectanguloAltura = 5,
                PerimetroRectanguloBase = 5
            };
            var instruccion = new InstruccionVueloDto
            {
                Acciones = new List<string> { "L"},
                CoordenadaVuelo = new CoordenadaVuelo { PuntoX = 3, PuntoY = 3, Direccion = "E" }
            };

            peticionDto.InstruccionesDto.Add(instruccion);

            //// prueba
            var response = await client.PostAsync("/api/ControlForestal", new StringContent(JsonConvert.SerializeObject(peticionDto), System.Text.Encoding.UTF8, "application/json"));

            var result = JsonConvert.DeserializeObject<ResponseVueloDto>(await response.Content.ReadAsStringAsync());
            
            //// Verificación
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, result.CoordenadasFinales.Count);
            Assert.AreEqual(result.CoordenadasFinales[0].PuntoX, 3 );
            Assert.AreEqual(result.CoordenadasFinales[0].PuntoY, 3);
            Assert.AreEqual(result.CoordenadasFinales[0].Direccion, "N");

        }

        [TestMethod]
        public async Task Post_SiLosValoresDeVueloIngresadoSonValidos_Caso02_SeNosRetornaUn200()
        {
            //// preparación
            _factory = new WebApplicationFactory<Startup>();
            var client = _factory.CreateClient();

            var peticionDto = new PeticionDto
            {
                PerimetroRectanguloAltura = 5,
                PerimetroRectanguloBase = 5
            };
            var instruccion = new InstruccionVueloDto
            {
                Acciones = new List<string> { "L" },
                CoordenadaVuelo = new CoordenadaVuelo { PuntoX = 3, PuntoY = 3, Direccion = "E" }
            };

            var instruccion2 = new InstruccionVueloDto
            {
                Acciones = new List<string> { "M", "M", "R", "M", "M", "R", "M", "R", "R", "M" },
                CoordenadaVuelo = new CoordenadaVuelo { PuntoX = 3, PuntoY = 3, Direccion = "E" }
            };

            var instruccion3 = new InstruccionVueloDto
            {
                Acciones = new List<string> { "L", "M", "L", "M", "L", "M", "L", "M", "M", "L", "M", "L", "M", "L", "M", "L", "M", "M" },
                CoordenadaVuelo = new CoordenadaVuelo { PuntoX = 1, PuntoY = 2, Direccion = "N" }
            };

            peticionDto.InstruccionesDto.Add(instruccion);
            peticionDto.InstruccionesDto.Add(instruccion2);
            peticionDto.InstruccionesDto.Add(instruccion3);

            //// prueba
            var response = await client.PostAsync("/api/ControlForestal", new StringContent(JsonConvert.SerializeObject(peticionDto), System.Text.Encoding.UTF8, "application/json"));

            var result = JsonConvert.DeserializeObject<ResponseVueloDto>(await response.Content.ReadAsStringAsync());

            //// Verificación
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(3, result.CoordenadasFinales.Count);

            Assert.AreEqual(result.CoordenadasFinales[0].PuntoX, 3);
            Assert.AreEqual(result.CoordenadasFinales[0].PuntoY, 3);
            Assert.AreEqual(result.CoordenadasFinales[0].Direccion, "N");

            Assert.AreEqual(result.CoordenadasFinales[1].PuntoX, 5);
            Assert.AreEqual(result.CoordenadasFinales[1].PuntoY, 1);
            Assert.AreEqual(result.CoordenadasFinales[1].Direccion, "E");

            Assert.AreEqual(result.CoordenadasFinales[2].PuntoX, 1);
            Assert.AreEqual(result.CoordenadasFinales[2].PuntoY, 4);
            Assert.AreEqual(result.CoordenadasFinales[2].Direccion, "N");


        }
    }
}
