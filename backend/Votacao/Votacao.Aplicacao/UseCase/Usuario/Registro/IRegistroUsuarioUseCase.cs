using Votacao.Comunicacao.Request.Usuario;
using Votacao.Comunicacao.Response.Usuario;

namespace Votacao.Aplicacao.UseCase.Usuario.Registro
{
    public interface IRegistroUsuarioUseCase
    {
        Task<RegistroUsuarioResponseJson> Execute(RegistroUsuarioRequestJson request);
    }
}
