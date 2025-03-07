using Votacao.Comunicacao.Response.Usuario;

namespace Votacao.Aplicacao.UseCase.Usuario.Excluir
{
    public interface IExcluirUsuarioUseCase
    {
        Task<RegistroUsuarioResponseJson> Execute(int id);
    }
}
