using UsuarioEntidade =  Votacao.Dominio.Entities;

namespace Votacao.Aplicacao.UseCase.Usuario.Lista.Todos
{
    public interface IListaTodosUsuariosUseCase
    {
        Task<List<UsuarioEntidade.Usuario>> Execute();
    }
}
