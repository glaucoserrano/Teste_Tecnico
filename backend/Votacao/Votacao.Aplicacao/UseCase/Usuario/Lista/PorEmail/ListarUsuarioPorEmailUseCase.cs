using Votacao.Comunicacao.Response.Usuario;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Usuario.Lista.PorEmail
{
    public class ListarUsuarioPorEmailUseCase: IListarUsuarioPorEmailUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ListarUsuarioPorEmailUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RegistroUsuarioResponseJson> Execute(string email)
        {
            var usuario = await _usuarioRepository.ListarUsuarioPorEmail(email);

            if (usuario == null)
                return new RegistroUsuarioResponseJson
                {
                    retorno = false,
                    mensagem = "Usuário não encontrado",
                    Usuario = null
                };
            return new RegistroUsuarioResponseJson
            {
                retorno = true,
                mensagem = "Usuário encontrado",
                Usuario = usuario
            };
        }
    }
}
