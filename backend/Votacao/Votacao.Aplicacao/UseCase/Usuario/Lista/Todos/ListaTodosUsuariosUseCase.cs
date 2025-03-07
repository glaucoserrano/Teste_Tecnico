
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Usuario.Lista.Todos
{
    public class ListaTodosUsuariosUseCase : IListaTodosUsuariosUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ListaTodosUsuariosUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Dominio.Entities.Usuario>> Execute()
        {
            var usuarios = await _usuarioRepository.ListaTodosUsuarios();

            return usuarios;
        }
    }
}
