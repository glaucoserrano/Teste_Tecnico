using Microsoft.EntityFrameworkCore;
using Votacao.Dominio.Repositories;

namespace Votacao.Infraestrutura.DataAcess.Repositories.Voto
{
    public class VotoRepository : IVotoRepository
    {
        private readonly VotacaoDbContext _context;

        public VotoRepository(VotacaoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UsuarioJaVotou(int UsuarioId, DateTime hoje)
        {
            var usuarioVotou = await _context.Votos.AnyAsync(voto => voto.UsuarioId == UsuarioId &&
            voto.DiaVoto == hoje);

            if (usuarioVotou)
                return true;

            return false;
        }
        public async Task<bool> RestauranteEscolhidoSemana(int RestauranteId, DateTime hoje)
        {
            var inicioSemana = hoje.AddDays(-(int)hoje.DayOfWeek);
            var restauranteEscolhidoSemana = await _context.Votos.AnyAsync(
                voto => voto.RestauranteId == RestauranteId &&
                voto.DiaVoto >= inicioSemana);

            if (restauranteEscolhidoSemana)
                return true;

            return false;
        }

        public async Task RegistrarVoto(Dominio.Entities.Voto voto)
        {
            await _context.Votos.AddAsync(voto);
            await _context.SaveChangesAsync();
        }

        public async Task<Dominio.Entities.Restaurante?> ListarResultadoRestauranteHoje()
        {
            var hoje = DateTime.UtcNow.Date;

            var resultado = await _context.Votos
                .Where(voto => voto.DiaVoto == hoje)
                .GroupBy(voto => voto.RestauranteId)
                .OrderByDescending(voto => voto.Count())
                .Select(voto => voto.Key)
                .FirstOrDefaultAsync();

            if(resultado == 0)
                return null;

            return await _context.Restaurantes.FindAsync(resultado);
        }
    }
}
