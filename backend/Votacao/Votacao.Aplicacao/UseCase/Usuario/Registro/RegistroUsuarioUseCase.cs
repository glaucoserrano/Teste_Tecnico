using Votacao.Comunicacao.Request.Usuario;
using Votacao.Comunicacao.Response.Usuario;
using UsuarioEntidade = Votacao.Dominio.Entities;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Usuario.Registro
{
    public class RegistroUsuarioUseCase : IRegistroUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RegistroUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RegistroUsuarioResponseJson> Execute(RegistroUsuarioRequestJson request)
        {
            var EmailExiste = await _usuarioRepository.VerificarEmailExiste(request.Email);

            if(!EmailExiste)
                return new RegistroUsuarioResponseJson{
                    retorno = false,
                    mensagem = "Email ja cadastrado",
                    Usuario = null
                };
            var usuario = new UsuarioEntidade.Usuario
            {
                Nome = request.Nome,
                Email = request.Email
            };

            await _usuarioRepository.AdicionarUsuario(usuario);

            return new RegistroUsuarioResponseJson
            {
                retorno = true,
                mensagem = "Usuario Cadastrado com sucesso",
                Usuario = usuario
            };
        }
    }
}
