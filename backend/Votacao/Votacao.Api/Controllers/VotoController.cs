using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Votacao.Aplicacao.UseCase.Voto.Registro;
using Votacao.Aplicacao.UseCase.Voto.Resultado;
using Votacao.Comunicacao.Request.Voto;
using Votacao.Comunicacao.Response.Restaurante;
using Votacao.Comunicacao.Response.Voto;

namespace Votacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegistroVotoResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroVotoResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarVoto([FromServices] IRegistroVotoUseCase useCase,
            [FromBody] RegistroVotoRequestJson request)
        {
            var response = await useCase.Execute(request);
            if (!response.retorno)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("resultado")]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarResultado([FromServices] IResultadoVotoUseCase useCase)
        {
            var response = await useCase.Execute();
            if (!response.retorno)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
