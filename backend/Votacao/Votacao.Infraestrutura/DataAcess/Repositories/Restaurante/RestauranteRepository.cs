using Microsoft.EntityFrameworkCore;
using Votacao.Dominio.Entities;
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

        public async Task<List<Dominio.Entities.Restaurante>> ListarTodosRestaurantes()
        {
            return await _context.Restaurantes.AsNoTracking().ToListAsync();
        }
        public async Task<Dominio.Entities.Restaurante> ListarRestaurantePorId(int id)
        {
            var restaurante = await _context.Restaurantes.FirstOrDefaultAsync(restaurante =>
            restaurante.Id == id);

            return restaurante;
        }

        public async Task<bool> EditarRestaurante(int id, Dominio.Entities.Restaurante restaurante)
        {
            var restauranteExiste = await ListarRestaurantePorId(id);

            if (restauranteExiste == null)
                return false;

            restauranteExiste.Nome = restaurante.Nome;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExcluirRestaurante(int id)
        {
            var restauranteExiste = await ListarRestaurantePorId(id);

            if (restauranteExiste == null)
                return false;

            _context.Restaurantes.Remove(restauranteExiste);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<Dominio.Entities.Restaurante?>> ListarRestaurantesDisponiveis()
        {
            var hoje = DateTime.UtcNow;
            var todosRestaurantes = await _context.Restaurantes.AsNoTracking().ToListAsync();
            var inicioSemana = hoje.AddDays(-(int)hoje.DayOfWeek);

            var votosSemana = await _context.Votos
                .Where(voto => voto.DiaVoto >= inicioSemana )
                .Select(voto => voto.RestauranteId)
                .ToListAsync();

            var restaurantesDisponiveis = todosRestaurantes
                .Where(restaurante => !votosSemana.Contains(restaurante.Id))
                .ToList();

            return restaurantesDisponiveis;
        }
    }
}
