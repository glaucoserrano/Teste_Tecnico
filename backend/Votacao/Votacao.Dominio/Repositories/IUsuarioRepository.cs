using Votacao.Dominio.Entities;

namespace Votacao.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<bool> VerificarEmailExiste(string email);
        Task AdicionarUsuario(Usuario usuario);
        Task<List<Usuario>> ListaTodosUsuarios();
    }
}
