using Votacao.Comunicacao.Response.Usuario;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Usuario.Editar
{
    public class EditarUsuarioUseCase : IEditarUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public EditarUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RegistroUsuarioResponseJson> Execute(int Id, Dominio.Entities.Usuario usuario)
        {
            var usuarioExiste = await _usuarioRepository.EditarUsuario(Id, usuario);

            if(!usuarioExiste)
                return new RegistroUsuarioResponseJson
                {
                    retorno = false,
                    mensagem = "Usuario não encontrado",
                    Usuario = null
                };

            var usuarioEditado = await _usuarioRepository.ListarUsuarioPorId(Id);
            return new RegistroUsuarioResponseJson
            {
                retorno = true,
                mensagem = "Usuario editado com sucesso",
                Usuario = usuarioEditado
            };
        }
    }
}
