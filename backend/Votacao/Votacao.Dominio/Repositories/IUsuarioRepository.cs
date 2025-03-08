using Votacao.Dominio.Entities;

namespace Votacao.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<bool> VerificarEmailExiste(string email);
        Task AdicionarUsuario(Usuario usuario);
        Task<List<Usuario>> ListaTodosUsuarios();

        Task<Usuario> ListarUsuarioPorId(int id);
        Task<Usuario> ListarUsuarioPorEmail(string email);
        Task<bool> EditarUsuario(int id, Usuario usuario);
        Task<bool> ExcluirUsuario(int id);
    }
}
