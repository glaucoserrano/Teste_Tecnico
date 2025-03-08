using Votacao.Comunicacao.Response.Usuario;

namespace Votacao.Aplicacao.UseCase.Usuario.Lista.PorEmail
{
    public interface IListarUsuarioPorEmailUseCase
    {
        Task<RegistroUsuarioResponseJson> Execute(string email);
    }
}
