using Microsoft.EntityFrameworkCore;
using Votacao.Dominio.Entities;
using Votacao.Dominio.Repositories;
using UsuarioEntidade = Votacao.Dominio.Entities;

namespace Votacao.Infraestrutura.DataAcess.Repositories.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly VotacaoDbContext _context;

        public UsuarioRepository(VotacaoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> VerificarEmailExiste(string email)
        {
            var emailExiste = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email);

            if (emailExiste != null)
                return false;

            return true;
        }
        public async Task AdicionarUsuario(UsuarioEntidade.Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UsuarioEntidade.Usuario>> ListaTodosUsuarios()
        {
            var usuarios = await _context.Usuarios.AsNoTracking().ToListAsync();

            return usuarios;
        }

        public async Task<UsuarioEntidade.Usuario> ListarUsuarioPorId(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario =>
            usuario.Id == id);

            return usuario;
        }
        public async Task<UsuarioEntidade.Usuario> ListarUsuarioPorEmail(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario =>
            usuario.Email == email);

            return usuario;
        }


        public async Task<bool> EditarUsuario(int id, UsuarioEntidade.Usuario usuario)
        {
            var usuarioExiste = await ListarUsuarioPorId(id);

            if (usuarioExiste == null)
                return false;

            usuarioExiste.Nome = usuario.Nome;
            usuarioExiste.Email = usuario.Email;

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> ExcluirUsuario(int id)
        {
            var usuarioExiste = await ListarUsuarioPorId(id);

            if (usuarioExiste == null)
                return false;

            _context.Usuarios.Remove(usuarioExiste);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
