using Votacao.Comunicacao.Response.Usuario;

namespace Votacao.Aplicacao.UseCase.Usuario.Lista.PorId
{
    public interface IListarUsuarioPorIdUseCase
    {
        Task<RegistroUsuarioResponseJson> Execute(int Id);
    }
}
