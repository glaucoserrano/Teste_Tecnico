using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Votacao.Dominio.DTO;
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

        public async Task<VencedorHojeDTO?> ListarResultadoRestauranteHoje()
        {
            var hoje = DateTime.UtcNow.Date;

            var resultado = await _context.Votos
                .Where(voto => voto.DiaVoto == hoje)
                .GroupBy(voto => voto.RestauranteId)
                .Select(voto => new
                {
                    RestauranteId = voto.Key,
                    TotalVotos = voto.Count()
                })
                .OrderByDescending(voto => voto.TotalVotos)
                .FirstOrDefaultAsync();

            if(resultado == null)
                return null;

            var restaurante = await _context.Restaurantes.FindAsync(resultado.RestauranteId);
            if (restaurante == null)
                return null;

            return new VencedorHojeDTO
            {
                Id = restaurante.Id,
                Nome = restaurante.Nome,
                TotalVotos = resultado.TotalVotos
            };
        }
        public async Task<List<RestauranteVencedorSemanaDTO>> ListarVencedoresSemana()
        {
            var inicioSemana = DateTime.UtcNow.Date.AddDays(-6);
            var hoje = DateTime.UtcNow.Date;

            var resultado = await _context.Votos
                .Where(voto => voto.DiaVoto >= inicioSemana && voto.DiaVoto <= hoje)
                .GroupBy(voto => new { voto.DiaVoto, voto.RestauranteId })
                .Select(grupo => new
                {
                    Data = grupo.Key.DiaVoto,
                    RestauranteId = grupo.Key.RestauranteId,
                    TotalVotos = grupo.Count()
                })
                .OrderBy(grupo => grupo.Data)
                .ToListAsync();

            var vencedoresPorDia = resultado
                .GroupBy(resultado => resultado.Data)
                .Select(grupo =>
                {
                    var vencedor = grupo.OrderByDescending(r => r.TotalVotos).First();
                    return new { grupo.Key, vencedor.RestauranteId, vencedor.TotalVotos };
                })
                .ToList();

            var listaVencedores = new List<RestauranteVencedorSemanaDTO>();

            foreach (var item in vencedoresPorDia)
            {
                var restaurante = await _context.Restaurantes.FindAsync(item.RestauranteId);
                if(restaurante != null)
                {
                    listaVencedores.Add(new RestauranteVencedorSemanaDTO
                    {
                        Data = item.Key,
                        RestauranteId = restaurante.Id,
                        RestauranteNome = restaurante.Nome,
                        TotalVotos = item.TotalVotos
                    });
                }
            }
            return listaVencedores;
        }
    }
}
