using Votacao.Comunicacao.Response.Usuario;
using Votacao.Dominio.Repositories;
namespace Votacao.Aplicacao.UseCase.Usuario.Lista.PorId
{
    public class ListarUsuarioPorIdUseCase : IListarUsuarioPorIdUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ListarUsuarioPorIdUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RegistroUsuarioResponseJson> Execute(int Id)
        {
            var usuario = await _usuarioRepository.ListarUsuarioPorId(Id);

            if(usuario == null)
                return new RegistroUsuarioResponseJson
                {
                    retorno = false,
                    mensagem = "Usuario não encontrado",
                    Usuario = null
                };
            return new RegistroUsuarioResponseJson
            {
                retorno = true,
                mensagem = "Usuario encontrado",
                Usuario = usuario
            };
        }
    }
}
