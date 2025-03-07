using Microsoft.AspNetCore.Mvc;
using Votacao.Aplicacao.UseCase.Restaurante.Editar;
using Votacao.Aplicacao.UseCase.Restaurante.Excluir;
using Votacao.Aplicacao.UseCase.Restaurante.Lista.PorId;
using Votacao.Aplicacao.UseCase.Restaurante.Lista.Todos;
using Votacao.Aplicacao.UseCase.Restaurante.Registro;
using Votacao.Aplicacao.UseCase.Usuario.Editar;
using Votacao.Aplicacao.UseCase.Usuario.Excluir;
using Votacao.Aplicacao.UseCase.Usuario.Lista.PorId;
using Votacao.Aplicacao.UseCase.Usuario.Lista.Todos;
using Votacao.Aplicacao.UseCase.Usuario.Registro;
using Votacao.Comunicacao.Request.Restaurante;
using Votacao.Comunicacao.Response.Restaurante;
using Votacao.Comunicacao.Response.Usuario;
using Votacao.Dominio.Entities;

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
        [HttpGet]
        [ProducesResponseType(typeof(List<Restaurante>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodosRestaurantes(
            [FromServices] IListarTodosRestaurantesUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListarRestaurantePorId(
    [FromServices] IListarRestaurantePorIdUseCase useCase,
    int id)
        {
            var response = await useCase.Execute(id);
            if (!response.retorno)
                return NotFound(response);

            return Ok(response);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditarRestaurante(
            [FromServices] IEditarRestauranteUseCase useCase,
            int id,
            [FromBody] Restaurante restaurante)
        {
            var response = await useCase.Execute(id, restaurante);
            if (!response.retorno)
                return NotFound(response);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroRestauranteResponseJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirRestaurante(
            [FromServices] IExcluirRestauranteUseCase useCase,
            int id)
        {
            var response = await useCase.Execute(id);
            if (!response.retorno)
                return NotFound(response);

            return Ok(response);
        }
    }
}
