using Microsoft.AspNetCore.Mvc;
using Votacao.Aplicacao.UseCase.Restaurante.Registro;
using Votacao.Aplicacao.UseCase.Usuario.Registro;
using Votacao.Comunicacao.Request.Restaurante;
using Votacao.Comunicacao.Response.Restaurante;

namespace Votacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistroUsuario(
            [FromServices] IRegistroRestauranteUseCase useCase, 
            [FromBody] RegistroRestauranteRequestJson request)
        {
            var response = await useCase.Execute(request);
            if (!response.retorno)
                return BadRequest(response);

            return Created(string.Empty, response);
        }
    }
}
