
using Votacao.Comunicacao.Response.Usuario;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Usuario.Excluir
{
    public class ExcluirUsuarioUseCase : IExcluirUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ExcluirUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RegistroUsuarioResponseJson> Execute(int id)
        { 
            var usuarioExcluido = await _usuarioRepository.ExcluirUsuario(id);

            if(!usuarioExcluido)
                return new RegistroUsuarioResponseJson
                {
                    retorno = false,
                    mensagem = "Usuario não encontrado",
                    Usuario = null
                };
            return new RegistroUsuarioResponseJson
            {
                retorno = true,
                mensagem = "Usuario excluido com sucesso",
                Usuario = null
            };
        }
    }
}
