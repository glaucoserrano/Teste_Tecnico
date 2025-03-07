using Votacao.Comunicacao.Response.Usuario;
using UsuarioEntidade = Votacao.Dominio.Entities;

namespace Votacao.Aplicacao.UseCase.Usuario.Editar
{
    public interface IEditarUsuarioUseCase
    {
        Task<RegistroUsuarioResponseJson> Execute(int Id, UsuarioEntidade.Usuario usuario);
    }
}
