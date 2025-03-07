using Microsoft.EntityFrameworkCore;
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
    }
}
