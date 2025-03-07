using Microsoft.EntityFrameworkCore;
using Votacao.Dominio.Repositories;

namespace Votacao.Infraestrutura.DataAcess.Repositories.Restaurante
{
    public class RestauranteRepository : IRestauranteRepository
    {
        private readonly VotacaoDbContext _context;

        public RestauranteRepository(VotacaoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> VerificarRestauranteExiste(string nome)
        {
            var restauranteExiste = await _context.Restaurantes.FirstOrDefaultAsync(restaurante =>
            restaurante.Nome == nome);

            if (restauranteExiste != null)
                return false;

            return true;

        }
        public async Task AdicionarRestaurante(Dominio.Entities.Restaurante restaurante)
        {
            await _context.Restaurantes.AddAsync(restaurante);
            await _context.SaveChangesAsync();
        }
    }
}
