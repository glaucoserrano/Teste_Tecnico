using Microsoft.AspNetCore.Mvc;
using Votacao.Aplicacao.UseCase.Usuario.Lista.Todos;
using Votacao.Aplicacao.UseCase.Usuario.Registro;
using Votacao.Comunicacao.Request.Usuario;
using Votacao.Comunicacao.Response.Usuario;
using Votacao.Dominio.Entities;
using Votacao.Dominio.Repositories;

namespace Votacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegistroUsuarioResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegistroUsuarioResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistroUsuario(
            [FromServices] IRegistroUsuarioUseCase useCase, 
            [FromBody] RegistroUsuarioRequestJson request)
        {
            var response = await useCase.Execute(request);
            if (!response.retorno)
                return BadRequest(response);

            return Created(string.Empty,response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Usuario>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodosUsuarios(
            [FromServices] IListaTodosUsuariosUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }
    }
}
